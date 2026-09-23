using TMPro;
using UnityEngine;

public class TutorialPrompt : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text tutorialText;
    [TextArea(2, 4)]
    [SerializeField] private string message;

    private void Awake()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (tutorialPanel == null || tutorialText == null)
            return;

        tutorialText.text = message;
        tutorialPanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }
}