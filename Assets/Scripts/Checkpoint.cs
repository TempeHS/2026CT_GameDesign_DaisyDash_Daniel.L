using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnManager respawn = other.GetComponent<RespawnManager>();
            if (respawn != null)
            {
                respawn.SetCheckpoint(spawnPoint.position);
            }
        }
    }
}
