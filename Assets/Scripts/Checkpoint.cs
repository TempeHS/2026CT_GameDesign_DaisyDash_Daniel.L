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

/* using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask groundLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnManager respawn = other.GetComponent<RespawnManager>();
            if (respawn != null)
            {
                Vector3 groundPos = FindGroundPosition();
                respawn.SetCheckpoint(groundPos);
            }
        }
    }

    private Vector3 FindGroundPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);

        if (hit.collider != null)
        {
            return new Vector3(hit.point.x, hit.point.y + 0.5f, transform.position.z);
        }

        return transform.position;
    }
}
*/ 