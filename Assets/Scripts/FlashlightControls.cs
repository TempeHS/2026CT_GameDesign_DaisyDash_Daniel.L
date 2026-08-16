using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class SmoothRotateToMouse : MonoBehaviour
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

    [Header("Flicker")]
    [SerializeField] private bool enableFlicker = true;
    [SerializeField] private float flickerChance = 0.08f;
    [SerializeField] private float blackoutChance = 0.02f;
    [SerializeField] private Vector2 flickerOffTime = new Vector2(0.03f, 0.1f);
    [SerializeField] private Vector2 blackoutOffTime = new Vector2(0.4f, 1.2f);
    [SerializeField] private Vector2 flickerInterval = new Vector2(0.05f, 0.2f);

    private Light2D light2D;

    private bool userLightOn = false;
    private bool flickerOn = false;

    private float nextFlickerCheck;
    private float flickerOffUntil;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
        light2D = GetComponent<Light2D>();

        light2D.enabled = false;

        if (flashlightParticles != null)
            flashlightParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        ScheduleNextCheck();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(toggleMouseButton))
        {
            userLightOn = !userLightOn;
            flickerOn = userLightOn;
            SetFlashlightState(userLightOn);
        }

        if (userLightOn && enableFlicker)
            UpdateFlicker();

        if (cam == null || player == null) return;

        RotateToMouse();
        FollowPlayer();
        SyncParticles();
    }

    private void UpdateFlicker()
    {
        if (!flickerOn && Time.time >= flickerOffUntil)
        {
            flickerOn = true;
            SetFlashlightState(true);
            ScheduleNextCheck();
        }

        if (flickerOn && Time.time >= nextFlickerCheck)
        {
            float roll = Random.value;

            if (roll < blackoutChance)
                TriggerOff(blackoutOffTime);
            else if (roll < blackoutChance + flickerChance)
                TriggerOff(flickerOffTime);
            else
                ScheduleNextCheck();
        }
    }

    private void TriggerOff(Vector2 range)
    {
        flickerOn = false;
        SetFlashlightState(false);
        flickerOffUntil = Time.time + Random.Range(range.x, range.y);
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
       flickerOn = false;
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

    private void ScheduleNextCheck()
    {
        nextFlickerCheck = Time.time + Random.Range(flickerInterval.x, flickerInterval.y);
    }
}
