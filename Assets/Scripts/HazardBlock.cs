using UnityEngine;

public class HazardBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RespawnPlayer(collision.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        // Reset timer if you want
        TimerManager timer = Object.FindFirstObjectByType<TimerManager>();
        if (timer != null)
        {
            timer.ResetTimer();
        }

        // Respawn at last checkpoint
        RespawnManager respawn = player.GetComponent<RespawnManager>();
        if (respawn != null)
        {
            respawn.Respawn();
        }
    }
}
