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

    [SerializeField] private float changeTextEverySecond = 0.3f;

    [SerializeField] private bool isAlerting;

    private void Start()
    {
        StartCoroutine(ChangeTideGaugeText());
    }
    IEnumerator ChangeTideGaugeText()
    {
        yield return new WaitForSeconds(changeTextEverySecond);
    }
}
