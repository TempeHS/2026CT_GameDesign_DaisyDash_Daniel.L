using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class FlashlightControls : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;
    [SerializeField] private ParticleSystem flashlightParticles;

    [Header("Settings")]
    [SerializeField] private float followDistance = 0.6f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float angleOffset = -90f;
    [SerializeField] private int toggleMouseButton = 0;

    private Light2D light2D;

    private bool userLightOn = false;

    public bool IsLightEmitting => light2D != null && light2D.enabled;
    public Vector2 BeamOrigin => transform.position;
    public Vector2 BeamDirection => transform.up;

    public Light2D Light => light2D;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
        light2D = GetComponent<Light2D>();

        light2D.enabled = false;

        if (flashlightParticles != null)
            flashlightParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(toggleMouseButton))
        {
            userLightOn = !userLightOn;
            SetFlashlightState(userLightOn);
        }

        if (cam == null || player == null) return;

        RotateToMouse();
        FollowPlayer();
        SyncParticles();
    }

    private void SetFlashlightState(bool on)
    {
        light2D.enabled = on;

        if (flashlightParticles == null) return;

        if (on)
        {
            if (!flashlightParticles.isPlaying)
                flashlightParticles.Play();
        }
        else
        {
            flashlightParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    public void TurnOffFlashlight()
    {
        userLightOn = false;
        SetFlashlightState(false);
    }

    private void RotateToMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.forward, new Vector3(0f, 0f, player.position.z));

        if (!plane.Raycast(ray, out float enter)) return;

        Vector3 mouseWorld = ray.GetPoint(enter);
        Vector2 dir = mouseWorld - player.position;

        if (dir.sqrMagnitude < 0.0001f) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleOffset;
        Quaternion target = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);
    }

    private void FollowPlayer()
    {
        Vector3 facing = transform.up;
        transform.position = player.position + facing * followDistance;
    }

    private void SyncParticles()
    {
        if (flashlightParticles != null)
            flashlightParticles.transform.position = transform.position;
    }

}
