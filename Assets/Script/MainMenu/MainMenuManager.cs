using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;


    private void Start()
    {
        AudioManager.instance.PlayBGM(AudioManager.instance.mainMenuBGM);
        AudioManager.instance.PlayAlarmSFX(true);
    }
    public void StartGame()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        if (SceneController.instance != null)
        {
            SceneController.instance.LoadSceneByIndexPlus();
        }
    }

    public void SettingPanel()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        if (UIManager.instance != null)
        {
            if (settingPanel.activeSelf)
            {
                UIManager.instance.ClosePanel(settingPanel);
            }
            else
            {
                UIManager.instance.ChangeAllPanel(settingPanel);
            }
        }

    }
    public void CreditPanel()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        if (UIManager.instance != null)
        {
            if (creditPanel.activeSelf)
            {
                UIManager.instance.ClosePanel(creditPanel);
            }
            else
            {
                UIManager.instance.ChangeAllPanel(creditPanel);
            }
        }
    }

    public void ExitGame()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        Application.Quit();
    }


}
