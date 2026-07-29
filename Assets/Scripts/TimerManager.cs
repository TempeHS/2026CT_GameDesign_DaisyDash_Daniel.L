using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("Timer UI")]
    public TextMeshProUGUI timerText; 

    private float elapsedTime;
    private bool timerRunning;

    void Start()
    {
        elapsedTime = 0f;
        UpdateTimerText();
    }

    void Update()
    {
        if (!timerRunning)
        {
            // Checks for movement keys or the shift key to kickstart the run automatically
            if (Input.GetAxisRaw("Horizontal") != 0 || 
                Input.GetAxisRaw("Vertical") != 0 || 
                Input.GetButtonDown("Jump") ||
                Input.GetKeyDown(KeyCode.LeftShift))
            {
                timerRunning = true;
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);

        timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    public void StopTimer() => timerRunning = false;

    public void ResetTimer()
    {
        timerRunning = false;
        elapsedTime = 0f;
        UpdateTimerText();
    }
}
