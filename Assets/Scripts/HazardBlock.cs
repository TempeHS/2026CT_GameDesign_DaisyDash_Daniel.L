using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetLevel();
        }
    }

    private void ResetLevel()
    {
        TimerManager timer = Object.FindFirstObjectByType<TimerManager>();
        if (timer != null)
        {
            timer.ResetTimer();
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
