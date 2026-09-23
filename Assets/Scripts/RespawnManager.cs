using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;

    private Vector3 respawnPoint;
    private Rigidbody2D rb;
    private bool isRespawning;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    public void SetCheckpoint(Vector3 newPoint)
    {
        respawnPoint = newPoint;
    }

    public void Respawn()
    {
        if (isRespawning)
            return;

        if (deathSound != null)
        {
            StartCoroutine(RespawnAfterDeathSound());
            return;
        }

        CompleteRespawn();
    }

    private IEnumerator RespawnAfterDeathSound()
    {
        isRespawning = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        yield return new WaitForSeconds(deathSound.length);

        if (rb != null)
            rb.simulated = true;

        CompleteRespawn();
        isRespawning = false;
    }

    private void CompleteRespawn()
    {
        transform.position = respawnPoint;
        GetComponentInChildren<FlashlightControls>().TurnOffFlashlight();


        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
