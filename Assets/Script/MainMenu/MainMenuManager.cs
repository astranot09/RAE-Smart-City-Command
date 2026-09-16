using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;



    public void StartGame()
    {
        if (SceneController.instance != null)
        {
            SceneController.instance.LoadSceneByIndexPlus();
        }
    }

    public void SettingPanel()
    {
        if(UIManager.instance != null)
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
        Application.Quit();
    }


}
