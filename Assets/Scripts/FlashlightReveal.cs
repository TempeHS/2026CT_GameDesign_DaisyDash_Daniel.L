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

    private Renderer[] targetRenderers;
    private Collider2D[] colliders2D;
    private bool isRevealed;

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

        for (int i = 0; i < targetRenderers.Length; i++)
            targetRenderers[i].enabled = revealed;

        if (!toggleCollision) return;

        for (int i = 0; i < colliders2D.Length; i++)
            colliders2D[i].enabled = revealed;
    }

}
