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
        outpostLose.text = $"This {outpostLose.gameObject.name} reached population to 0";
        dayLose.text = $"Alive : {DayReportManager.instance.DayCurrent.ToString()} days";
    }

    public void LoseConfirm(OutpostManager outpostManager)
    {
        SceneController.instance.LoadSceneByName("MainMenu");
    }
}
