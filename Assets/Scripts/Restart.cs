using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoldToRestartBar : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode restartKey = KeyCode.R;
    [SerializeField] private float holdDuration = 3f;

    [Header("UI")]
    [SerializeField] private Image restartFillBar; // Assign in Inspector

    private float holdTime;

    void Awake()
    {
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
            holdTime += Time.unscaledDeltaTime; // works even if timeScale == 0
            UpdateBar();

            if (holdTime >= holdDuration)
                RestartLevel();
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

    private void RestartLevel()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}