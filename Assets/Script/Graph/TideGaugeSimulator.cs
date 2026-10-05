using UnityEngine;
using TMPro;
using System.Collections;

public class TideGaugeSimulator : MonoBehaviour
{
    [SerializeField] private TMP_Text tideGaugeText;

    [SerializeField] private float minNormalSeaLevel;
    [SerializeField] private float maxNormalSeaLevel;

    [SerializeField] private float minAlertSeaLevel;
    [SerializeField] private float maxAlertSeaLevel;

    [SerializeField] private float valueSeaLevel;

    [SerializeField] private float changeTextEverySecond = 0.3f;

    [SerializeField] private bool isAlerting;
    [SerializeField] private bool onUnlock;
    private int level;
    [Header("Reference")]
    [SerializeField] private OutpostManager outpostManager;

    private void OnEnable()
    {
        outpostManager.DisasterManager.earlyStageOfDisasterEvent += DisasterFirstAlarmTrigger;
        outpostManager.DisasterManager.secondStageOfDisasterEvent += DisasterSecondAlarmTrigger;
        outpostManager.DisasterManager.finalStageOfDisasterEvent += DisasterFinalAlarmTrigger;
        outpostManager.cycleStart += CloseAllAlarmDisaster;
    }
    private void OnDisable()
    {
        outpostManager.DisasterManager.earlyStageOfDisasterEvent -= DisasterFirstAlarmTrigger;
        outpostManager.DisasterManager.secondStageOfDisasterEvent -= DisasterSecondAlarmTrigger;
        outpostManager.DisasterManager.finalStageOfDisasterEvent -= DisasterFinalAlarmTrigger;
        outpostManager.cycleStart -= CloseAllAlarmDisaster;
    }

    public void TideGaugeSetUp(TMP_Text label)
    {
        tideGaugeText = label;
    }

    IEnumerator ChangeTideGaugeText()
    {
        while (onUnlock)
        {
            ChangeValueSeaLevel();
            if(tideGaugeText != null)
                tideGaugeText.text = valueSeaLevel.ToString();
            yield return new WaitForSeconds(changeTextEverySecond);
        }

    }

    private void ChangeValueSeaLevel()
    {
        if (isAlerting)
        {
            valueSeaLevel = Random.Range(minAlertSeaLevel, maxAlertSeaLevel);
        }
        else
        {
            valueSeaLevel = Random.Range(minNormalSeaLevel, maxNormalSeaLevel);
        }

    }

    public void Upgrade()
    {
        Debug.Log("Upgrade");
        level++;
        switch (level)
        {
            case 1:
                onUnlock = true;
                StartCoroutine(ChangeTideGaugeText());
                break;
        }
    }

    public void DisasterSecondAlarmTrigger()
    {
        // Versi Normal
        if ((outpostManager.DisasterManager.CurrentDisaster == DisasterType.Tsunami) && !isAlerting)
            isAlerting = true;
    }
    public void DisasterFirstAlarmTrigger()
    {
        //level 1
        if ((level >= 3 || outpostManager.DisasterManager.CurrentDisaster == DisasterType.Tsunami) && !isAlerting)
            isAlerting = true;
    }


    public void DisasterFinalAlarmTrigger()
    {
        //level 2
        if (level >= 2 || outpostManager.DisasterManager.CurrentDisaster == DisasterType.Tsunami)
            Debug.Log("Predicted : TideGauge");
    }
    public void CloseAllAlarmDisaster()
    {
        isAlerting = false;
    }

    public void TideGaugeReset()
    {
        tideGaugeText = null;
    }
}
