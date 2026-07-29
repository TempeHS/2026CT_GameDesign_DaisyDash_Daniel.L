using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the finish zone is the Player
        if (other.CompareTag("Player"))
        {
            // Find the timer script in the scene and turn it off
            TimerManager timer = Object.FindFirstObjectByType<TimerManager>();
            
            if (timer != null)
            {
                timer.StopTimer();
            }
        }
    }
}
