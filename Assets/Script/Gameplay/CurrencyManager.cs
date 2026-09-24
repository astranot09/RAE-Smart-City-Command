using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private int currencyCurrent;
    public int CurrencyCurrent => currencyCurrent;

    public void ChangeCurrency(int value)
    {
        currencyCurrent += value;
    }

}
