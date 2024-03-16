using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [Header("Starting Amount")]
    public int startingAmount = 100;

    public static int money = 0;

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


        SetMoneyAmount(startingAmount);
    }

    #region Currency Functions
    public static void AddMoney(int amount)
    {
        money += amount;

        OnCurrencyChanged?.Invoke(money);
    }
    public static void RemoveMoney(int amount)
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
