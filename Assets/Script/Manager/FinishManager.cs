using UnityEngine;

public class FinishManager : MonoBehaviour
{
    public static FinishManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private GameObject winPanel;

    public void WinSetUp()
    {
        UIManager.instance.OpenPanel(winPanel);
    }
    public void BackToMainMenu()
    {
        SceneController.instance.LoadSceneByName("MainMenu");
    }
}
