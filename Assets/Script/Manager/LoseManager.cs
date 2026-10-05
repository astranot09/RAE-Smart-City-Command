using TMPro;
using UnityEngine;

public class LoseManager : MonoBehaviour
{
    public static LoseManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private GameObject losePanel;

    [SerializeField] private TMP_Text outpostLose;
    [SerializeField] private TMP_Text dayLose;


    public void LoseSetUp(OutpostManager outpostManager)
    {
        UIManager.instance.OpenPanel(losePanel);
        UIManager.instance.CanOpenPanel(false);
        AudioManager.instance.PlayBGM(AudioManager.instance.lostBGM);
        outpostLose.text = $"This {outpostLose.gameObject.name} reached population to 0";
        dayLose.text = $"Alive : {DayReportManager.instance.DayCurrent.ToString()} days";
    }

    public void LoseConfirm()
    {
        UIManager.instance.CanOpenPanel(true);
        UIManager.instance.CloseAllPanels();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        SceneController.instance.LoadSceneByName("MainMenu");
    }
}
