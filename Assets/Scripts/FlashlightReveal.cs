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

    private Renderer rend;
    private Collider2D targetCollider;
    private Collider2D[] colliders2D;
    private bool isRevealed;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        targetCollider = GetComponent<Collider2D>();

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

    private Vector2[] BuildLightConePolygon(Light2D light, int segments = 24)
    {
        float angle = light.pointLightOuterAngle;
        float radius = light.pointLightOuterRadius;

        Vector2 origin = light.transform.position;

        float half = angle * 0.5f;

        Vector2[] poly = new Vector2[segments + 2];
        poly[0] = origin;

        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            float a = Mathf.Lerp(-half, half, t);

            Quaternion rot = Quaternion.Euler(0, 0, a);
            Vector2 localDir = rot * Vector2.up;

            Vector2 worldDir = light.transform.TransformDirection(localDir);

            poly[i + 1] = origin + worldDir * radius;
        }

        return poly;
    }

    private bool PointInPolygon(Vector2 p, Vector2[] poly)
    {
        bool inside = false;

        for (int i = 0, j = poly.Length - 1; i < poly.Length; j = i++)
        {
            bool intersect =
                ((poly[i].y > p.y) != (poly[j].y > p.y)) &&
                (p.x < (poly[j].x - poly[i].x) * (p.y - poly[i].y) /
                (poly[j].y - poly[i].y) + poly[i].x);

            if (intersect)
                inside = !inside;
        }

        return inside;
    }

    private bool CanReveal()
    {
        if (flashlight == null || !flashlight.IsLightEmitting)
            return false;

        Vector2 origin = flashlight.BeamOrigin;
        Light2D light = flashlight.Light;

        Vector2[] conePoly = BuildLightConePolygon(light);
        if (conePoly == null || conePoly.Length < 3)
            return false;

        Bounds b = targetCollider.bounds;

        Vector2[] pts = new Vector2[]
        {
            b.center,
            new Vector2(b.min.x, b.min.y),
            new Vector2(b.min.x, b.max.y),
            new Vector2(b.max.x, b.min.y),
            new Vector2(b.max.x, b.max.y),
            new Vector2(b.center.x, b.min.y),
            new Vector2(b.center.x, b.max.y),
            new Vector2(b.min.x, b.center.y),
            new Vector2(b.max.x, b.center.y)
        };

        for (int i = 0; i < pts.Length; i++)
        {
            Vector2 pt = pts[i];

            if (!PointInPolygon(pt, conePoly))
                continue;

            float dist = Vector2.Distance(origin, pt);
            if (dist > revealDistance)
                continue;

            Vector2 dir = (pt - origin).normalized;
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, dist, occluderMask);

            if (hit.collider == null)
                return true;
        }

        return false;
    }

    private void SetRevealed(bool revealed)
    {
        isRevealed = revealed;
        rend.enabled = revealed;

        if (!toggleCollision) return;

        for (int i = 0; i < colliders2D.Length; i++)
            colliders2D[i].enabled = revealed;
    }

    void OnDrawGizmos()
    {
        if (flashlight == null || flashlight.Light == null) return;

        Vector2[] poly = BuildLightConePolygon(flashlight.Light);
        if (poly == null) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < poly.Length - 1; i++)
            Gizmos.DrawLine(poly[i], poly[i + 1]);
    }
}
