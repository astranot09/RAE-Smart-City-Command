using UnityEngine;

public class PausedManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void OpenPausePanel()
    {
        if (!pausePanel.activeSelf)
        {
            UIManager.instance.OpenPanel(pausePanel);
            UIManager.instance.CanOpenPanel(false);
        }
        else if (pausePanel.activeSelf)
        {
            UIManager.instance.ClosePanel(pausePanel);
            UIManager.instance.CanOpenPanel(true);
        }
    }
}
