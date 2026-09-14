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
        settingPanel.SetActive(!settingPanel.activeSelf);
        Debug.Log("Game ini ga ada Setting :v");
    }
    public void CreditPanel()
    {
        creditPanel.SetActive(!creditPanel.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
