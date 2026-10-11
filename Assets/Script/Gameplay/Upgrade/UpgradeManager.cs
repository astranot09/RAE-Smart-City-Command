using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Upgrade
{
    public int price;
    public Sprite icon;
    public string upgradeName;
    public string upgradeDescription;
    public UnityEvent upgradeEvents;
}

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Panel")]
    [SerializeField] private GameObject upgradePanel;

    [Header("UI")]
    [SerializeField] private TMP_Text currencyCurrent;
    [SerializeField] private GameObject orbUpgradePrefab;

    [SerializeField] private TMP_Text descriptionText;


    [Header("Animation")]
    [SerializeField] private Transform upgradePanelTransform;
    [SerializeField] private float animDuration = 0.3f;

    [SerializeField] private Vector3 startScale = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private Vector3 targetScale = new Vector3(1f, 1f, 1f);
    private bool onAnimation = false;

    private void Start()
    {
        
    }

    public void UpgradeSetUp()
    {
        UIManager.instance.OpenPanel(upgradePanel);
        UpdateCurrencyUI();
        PlayAnimationEntry();
    }

    public void DescriptionSetUp(Upgrade upgrade)
    {
        descriptionText.text = upgrade.upgradeDescription;
    }

    public void SuccessUpgrade(UpgradePrefabManager upgradePrefabManager)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.upgrade);
        Instantiate(orbUpgradePrefab, upgradePrefabManager.orbUpgradeSpawner);
        UpdateCurrencyUI();
    }

    private void UpdateCurrencyUI()
    {
        if (CurrencyManager.instance != null)
            currencyCurrent.text = CurrencyManager.instance.CurrencyCurrent.ToString();
    }

    public void ClosePanel()
    {
        PlayAnimationExit();
    }

    private void PlayAnimationEntry()
    {
        onAnimation = true;
        upgradePanelTransform.DOKill();

        Sequence s = DOTween.Sequence();
        s.Append(upgradePanelTransform.DOScale(targetScale, animDuration))
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                upgradePanelTransform.DOKill();
                onAnimation = false;
            });
    }

    private void PlayAnimationExit()
    {
        onAnimation = true;
        upgradePanelTransform.DOKill();

        Sequence s = DOTween.Sequence();
        s.Append(upgradePanelTransform.DOScale(startScale, animDuration))
            .SetEase(Ease.InQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                upgradePanelTransform.DOKill();
                onAnimation = false;
                UIManager.instance.ClosePanel(upgradePanel);
            });
    }


}
