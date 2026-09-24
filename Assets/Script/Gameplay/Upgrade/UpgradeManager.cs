using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class Upgrade
{
    public int price;
    public Sprite icon;
    public string upgradeName;
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

    [Header("UI -- Type 1")]
    [SerializeField] private Image iconUpgradeImage_Type1;
    [SerializeField] private TMP_Text upgradeName_Type1;

    [SerializeField] private Transform orbUpgradeSpawner_Type1;
    [SerializeField] private TMP_Text priceUpgrade_Type1;
    [SerializeField] private int level_Type1;
    [SerializeField] private int maxLevel_Type1 = 4;
    [SerializeField] private List<Upgrade> upgrade_Type1;

    [Header("UI -- Type 2")]
    [SerializeField] private Image iconUpgradeImage_Type2;
    [SerializeField] private TMP_Text upgradeName_Type2;

    [SerializeField] private Transform orbUpgradeSpawner_Type2;
    [SerializeField] private TMP_Text priceUpgrade_Type2;
    [SerializeField] private int level_Type2;
    [SerializeField] private int maxLevel_Type2 = 4;
    [SerializeField] private List<Upgrade> upgrade_Type2;

    [Header("UI -- Type 3")]
    [SerializeField] private Image iconUpgradeImage_Type3;
    [SerializeField] private TMP_Text upgradeName_Type3;

    [SerializeField] private Transform orbUpgradeSpawner_Type3;
    [SerializeField] private TMP_Text priceUpgrade_Type3;
    [SerializeField] private int level_Type3;
    [SerializeField] private int maxLevel_Type3 = 4;
    [SerializeField] private List<Upgrade> upgrade_Type3;

    [Header("UI -- Type 4")]
    [SerializeField] private Image iconUpgradeImage_Type4;
    [SerializeField] private TMP_Text upgradeName_Type4;

    [SerializeField] private Transform orbUpgradeSpawner_Type4;
    [SerializeField] private TMP_Text priceUpgrade_Type4;
    [SerializeField] private int level_Type4;
    [SerializeField] private int maxLevel_Type4 = 4;
    [SerializeField] private List<Upgrade> upgrade_Type4;


    private void Start()
    {
        maxLevel_Type1 = upgrade_Type1.Count;
        maxLevel_Type2 = upgrade_Type2.Count;
        maxLevel_Type3 = upgrade_Type3.Count;
        maxLevel_Type4 = upgrade_Type4.Count;
    }

    public void UpgradeSetUp()
    {
        UIManager.instance.OpenPanel(upgradePanel);
        UpdateUpgradeUI();
    }


    public void UpgradeType1()
    {
        if (level_Type1 >= maxLevel_Type1)
        {
            Debug.Log("Udah Max Level Type 1");
            return;
        }

        if (CurrencyManager.instance.CurrencyCurrent >= upgrade_Type1[level_Type1].price)
        {
            CurrencyManager.instance.ChangeCurrency(-upgrade_Type1[level_Type1].price);

            upgrade_Type1[level_Type1].upgradeEvents?.Invoke();

            Instantiate(orbUpgradePrefab, orbUpgradeSpawner_Type1);

            level_Type1++;
            UpdateUpgradeUI();
        }
        else
        {
            Debug.Log("Gak cukup duit untuk upgrade Tipe 1");
        }
    }

    public void UpgradeType2()
    {
        if (level_Type2 >= maxLevel_Type2)
        {
            Debug.Log("Udah Max Level Type 2");
            return;
        }

        if (CurrencyManager.instance.CurrencyCurrent >= upgrade_Type2[level_Type2].price)
        {
            CurrencyManager.instance.ChangeCurrency(-upgrade_Type2[level_Type2].price);

            upgrade_Type2[level_Type2].upgradeEvents?.Invoke();

            Instantiate(orbUpgradePrefab, orbUpgradeSpawner_Type2);

            level_Type2++;
            UpdateUpgradeUI();
        }
        else
        {
            Debug.Log("Gak cukup duit untuk upgrade Tipe 2");
        }
    }

    public void UpgradeType3()
    {
        if (level_Type3 >= maxLevel_Type3)
        {
            Debug.Log("Udah Max Level Type 3");
            return;
        }

        if (CurrencyManager.instance.CurrencyCurrent >= upgrade_Type3[level_Type3].price)
        {
            CurrencyManager.instance.ChangeCurrency(-upgrade_Type3[level_Type3].price);

            upgrade_Type3[level_Type3].upgradeEvents?.Invoke();

            Instantiate(orbUpgradePrefab, orbUpgradeSpawner_Type3);

            level_Type3++;
            UpdateUpgradeUI();
        }
        else
        {
            Debug.Log("Gak cukup duit untuk upgrade Tipe 3");
        }
    }

    public void UpgradeType4()
    {
        if (level_Type4 >= maxLevel_Type4)
        {
            Debug.Log("Udah Max Level Type 4");
            return;
        }

        if (CurrencyManager.instance.CurrencyCurrent >= upgrade_Type4[level_Type4].price)
        {
            CurrencyManager.instance.ChangeCurrency(-upgrade_Type4[level_Type4].price);

            upgrade_Type3[level_Type4].upgradeEvents?.Invoke();

            Instantiate(orbUpgradePrefab, orbUpgradeSpawner_Type4);

            level_Type4++;
            UpdateUpgradeUI();
        }
        else
        {
            Debug.Log("Gak cukup duit untuk upgrade Tipe 4");
        }
    }

    private void UpdateUpgradeUI()
    {
        if (CurrencyManager.instance != null)
            currencyCurrent.text = CurrencyManager.instance.CurrencyCurrent.ToString();

        // --- Type 1 ---
        if (level_Type1 < maxLevel_Type1)
        {
            if (upgrade_Type1[level_Type1].icon != null)
                iconUpgradeImage_Type1.sprite = upgrade_Type1[level_Type1].icon;

            if (!string.IsNullOrEmpty(upgrade_Type1[level_Type1].upgradeName))
                upgradeName_Type1.text = upgrade_Type1[level_Type1].upgradeName;

            priceUpgrade_Type1.text = upgrade_Type1[level_Type1].price.ToString();
        }
        else
        {
            priceUpgrade_Type1.text = "MAX";
        }

        // --- Type 2 ---
        if (level_Type2 < maxLevel_Type2)
        {
            if (upgrade_Type2[level_Type2].icon != null)
                iconUpgradeImage_Type2.sprite = upgrade_Type2[level_Type2].icon;

            if (!string.IsNullOrEmpty(upgrade_Type2[level_Type2].upgradeName))
                upgradeName_Type2.text = upgrade_Type2[level_Type2].upgradeName;

            priceUpgrade_Type2.text = upgrade_Type2[level_Type2].price.ToString();
        }
        else
        {
            priceUpgrade_Type2.text = "MAX";
        }

        // --- Type 3 ---
        if (level_Type3 < maxLevel_Type3)
        {
            if (upgrade_Type3[level_Type3].icon != null)
                iconUpgradeImage_Type3.sprite = upgrade_Type3[level_Type3].icon;

            if (!string.IsNullOrEmpty(upgrade_Type3[level_Type3].upgradeName))
                upgradeName_Type3.text = upgrade_Type3[level_Type3].upgradeName;

            priceUpgrade_Type3.text = upgrade_Type3[level_Type3].price.ToString();
        }
        else
        {
            priceUpgrade_Type3.text = "MAX";
        }

        // --- Type 4 ---
        if (level_Type4 < maxLevel_Type3)
        {
            if (upgrade_Type4[level_Type4].icon != null)
                iconUpgradeImage_Type4.sprite = upgrade_Type4[level_Type4].icon;

            if (!string.IsNullOrEmpty(upgrade_Type4[level_Type4].upgradeName))
                upgradeName_Type4.text = upgrade_Type4[level_Type4].upgradeName;

            priceUpgrade_Type4.text = upgrade_Type3[level_Type4].price.ToString();
        }
        else
        {
            priceUpgrade_Type3.text = "MAX";
        }
    }

    public void ClosePanel()
    {
        UIManager.instance.ClosePanel(upgradePanel);
        Time.timeScale = 1;
    }

}
