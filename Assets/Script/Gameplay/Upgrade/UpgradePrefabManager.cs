using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class UpgradePrefabManager : MonoBehaviour, IPointerEnterHandler
{

    [Header("UI -- Type 1")]
    [SerializeField] private Image iconUpgradeImage_Type;
    [SerializeField] private TMP_Text upgradeName_Type;

    public Transform orbUpgradeSpawner;
    [SerializeField] private TMP_Text priceUpgrade_Type;
    [SerializeField] private int level_Type;

    [Header("Data -- Type 1")]
    [SerializeField] private int maxLevel_Type = 4;
    [SerializeField] private List<Upgrade> upgrade_Type;
    private void Start()
    {
        maxLevel_Type = upgrade_Type.Count;
    }
    private void OnEnable()
    {
        maxLevel_Type = upgrade_Type.Count;
    }
    public void UpdateUpgradeUI()
    {
        if (level_Type < maxLevel_Type)
        {
            if (upgrade_Type[level_Type].icon != null)
                iconUpgradeImage_Type.sprite = upgrade_Type[level_Type].icon;

            if (!string.IsNullOrEmpty(upgrade_Type[level_Type].upgradeName))
                upgradeName_Type.text = upgrade_Type[level_Type].upgradeName;

            priceUpgrade_Type.text = upgrade_Type[level_Type].price.ToString();
        }
        else
        {
            priceUpgrade_Type.text = "MAX";
        }
    }

    public void Upgrade()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        if (level_Type >= maxLevel_Type)
        {
            Debug.Log("Udah Max Level");
            return;
        }

        if (CurrencyManager.instance.CurrencyCurrent >= upgrade_Type[level_Type].price)
        {
            CurrencyManager.instance.ChangeCurrency(-upgrade_Type[level_Type].price);

            upgrade_Type[level_Type].upgradeEvents?.Invoke();

            level_Type++;
            UpgradeManager.instance.SuccessUpgrade(this);
            if (level_Type < maxLevel_Type)
            {
                UpgradeManager.instance.DescriptionSetUp(upgrade_Type[level_Type]);
            }
            UpdateUpgradeUI();
        }
        else
        {
            Debug.Log("Gak cukup duit untuk upgrade Tipe 1");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(level_Type < maxLevel_Type)
        {
            UpgradeManager.instance.DescriptionSetUp(upgrade_Type[level_Type]);
        }
    }


}
