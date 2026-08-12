using UnityEngine;
using UnityEngine.UI;

public class HoldToRestartBar : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode restartKey = KeyCode.R;
    [SerializeField] private float holdDuration = 3f;

    [Header("UI")]
    [SerializeField] private Image restartFillBar;

    private float holdTime;
    private RespawnManager respawnManager;

    void Awake()
    {
        // Find player’s RespawnManager
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            respawnManager = player.GetComponent<RespawnManager>();

        if (restartFillBar != null)
        {
            restartFillBar.type = Image.Type.Filled;
            restartFillBar.fillMethod = Image.FillMethod.Horizontal;
            restartFillBar.fillOrigin = (int)Image.OriginHorizontal.Left;
            restartFillBar.fillAmount = 0f;
        }
        else
        {
            Debug.LogWarning("HoldToRestartBar: restartFillBar is not assigned.");
        }
    }

    void Update()
    {
        if (Input.GetKey(restartKey))
        {
            holdTime += Time.unscaledDeltaTime;
            UpdateBar();

            if (holdTime >= holdDuration)
                RestartToCheckpoint();
        }
        else
        {
            holdTime = 0f;
            UpdateBar();
        }
    }

    private void UpdateBar()
    {
        if (restartFillBar == null) return;
        restartFillBar.fillAmount = Mathf.Clamp01(holdTime / holdDuration);
    }

    private void RestartToCheckpoint()
    {
        // Reset timer if you want
        TimerManager timer = Object.FindFirstObjectByType<TimerManager>();
        if (timer != null)
            timer.ResetTimer();

        // Respawn at checkpoint
        if (respawnManager != null)
            respawnManager.Respawn();
        else
            Debug.LogWarning("No RespawnManager found on Player.");
    }
}
