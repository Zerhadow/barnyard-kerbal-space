using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    //[Header("Stored Info")]
    [Tooltip("The amount of money the player has.")]
    [field: SerializeField]
    public int money { get; private set; } = 0;

    public static Action<int> OnCurrencyChanged = delegate { };

    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion

        transform.SetParent(null);
        DontDestroyOnLoad(Instance);

    }

    #region Currency Functions
    public void AddMoney(int amount)
    {
        money += amount;

        OnCurrencyChanged?.Invoke(money);
    }
    public void RemoveMoney(int amount)
    {
        money -= amount;
        if(money < 0) money = 0;

        OnCurrencyChanged?.Invoke(money);
    }
    public void SetMoneyAmount(int amount)
    {
        money = amount;
        if(money < 0) money = 0;

        OnCurrencyChanged?.Invoke(money);
    }

    #endregion
}
