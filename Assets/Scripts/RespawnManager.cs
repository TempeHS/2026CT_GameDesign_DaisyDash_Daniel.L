using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Rigidbody2D rb;

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
        transform.position = respawnPoint;
        GetComponentInChildren<SmoothRotateToMouse>().TurnOffFlashlight();


        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
