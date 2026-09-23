using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider2D))]
public class FlashlightReveal : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlashlightControls flashlight;

    [Header("Reveal Settings")]
    [SerializeField] private float revealDistance = 8f;
    [SerializeField] private LayerMask occluderMask;

    [Header("Collision")]
    [SerializeField] private bool toggleCollision = true;
    [SerializeField] private bool includeChildColliders = true;
    [SerializeField] private float minimumOverlapDepth = 0.05f;

    private Renderer[] targetRenderers;
    private Collider2D[] colliders2D;
    private readonly List<Collider2D> overlapResults = new List<Collider2D>();
    private bool isRevealed;
    private bool hasRespawnedPlayer;

    void Awake()
    {
        targetRenderers = includeChildColliders
            ? GetComponentsInChildren<Renderer>(true)
            : GetComponents<Renderer>();

        colliders2D = includeChildColliders
            ? GetComponentsInChildren<Collider2D>(true)
            : GetComponents<Collider2D>();

        SetRevealed(false);
    }

    void Update()
    {
        bool shouldReveal = CanReveal();
        if (shouldReveal != isRevealed)
            SetRevealed(shouldReveal);
    }

    private bool CanReveal()
    {
        if (flashlight == null || !flashlight.IsLightEmitting)
            return false;

        Vector2 origin = flashlight.BeamOrigin;
        Light2D light = flashlight.Light;

        if (targetRenderers.Length == 0)
            return false;

        Bounds bounds = targetRenderers[0].bounds;
        for (int i = 1; i < targetRenderers.Length; i++)
            bounds.Encapsulate(targetRenderers[i].bounds);

        const int sampleSteps = 4;
        for (int x = 0; x <= sampleSteps; x++)
        {
            float xPercent = x / (float)sampleSteps;
            for (int y = 0; y <= sampleSteps; y++)
            {
                float yPercent = y / (float)sampleSteps;
                Vector2 point = new Vector2(
                    Mathf.Lerp(bounds.min.x, bounds.max.x, xPercent),
                    Mathf.Lerp(bounds.min.y, bounds.max.y, yPercent));

                if (CanRevealPoint(point, origin, light))
                    return true;
            }
        }

        return false;
    }

    private bool CanRevealPoint(Vector2 point, Vector2 origin, Light2D light)
    {
        Vector2 toPoint = point - origin;
        float distance = toPoint.magnitude;

        if (distance <= 0.001f || distance > revealDistance)
            return false;

        if (Vector2.Angle(flashlight.BeamDirection, toPoint) > light.pointLightOuterAngle * 0.5f)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(origin, toPoint / distance, distance, occluderMask);
        return hit.collider == null;
    }

    private void SetRevealed(bool revealed)
    {
        isRevealed = revealed;
        if (!revealed)
            hasRespawnedPlayer = false;

        for (int i = 0; i < targetRenderers.Length; i++)
            targetRenderers[i].enabled = revealed;

        if (!toggleCollision) return;

        for (int i = 0; i < colliders2D.Length; i++)
            colliders2D[i].enabled = revealed;

        if (revealed)
            RespawnPlayerIfInside();
    }

    private void RespawnPlayerIfInside()
    {
        if (hasRespawnedPlayer)
            return;

        Physics2D.SyncTransforms();
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();

        for (int i = 0; i < colliders2D.Length; i++)
        {
            if (!colliders2D[i].enabled)
                continue;

            overlapResults.Clear();
            Physics2D.OverlapCollider(colliders2D[i], contactFilter, overlapResults);

            for (int j = 0; j < overlapResults.Count; j++)
            {
                ColliderDistance2D distance = Physics2D.Distance(colliders2D[i], overlapResults[j]);
                if (!distance.isOverlapped || distance.distance > -minimumOverlapDepth)
                    continue;

                RespawnManager respawn = overlapResults[j].GetComponentInParent<RespawnManager>();
                if (respawn == null || !respawn.CompareTag("Player"))
                    continue;

                hasRespawnedPlayer = true;
                respawn.Respawn();
                return;
            }
        }
    }

}
