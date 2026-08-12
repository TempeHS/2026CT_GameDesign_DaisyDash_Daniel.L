using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TimerManager timer = Object.FindFirstObjectByType<TimerManager>();
            if (timer != null)
            {
                timer.StopTimer();
            }
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                Debug.Log("Reached final level — no more scenes to load.");
            }
        }
    }
}
