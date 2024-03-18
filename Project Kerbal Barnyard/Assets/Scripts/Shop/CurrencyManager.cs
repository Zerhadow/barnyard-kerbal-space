using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [Header("Starting")]
    [SerializeField] private int _startingAmount = 100;

    [Header("Debug")]
    [SerializeField] private bool _debug = false;
    public bool DebugEnabled => _debug;

    public static int money = 0;
    private int speedDecreasing = 10;

    /// <summary>
    /// Check currency manager to change the multiplier.
    /// </summary>
    public static float StockMultiplier = 0.1f;

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


        SetMoneyAmount(_startingAmount);
    }
    private void Start()
    {
        SetMoneyAmount(_startingAmount);
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

    // will calculate the money earned from height and speed then add that to player disposal
    public void CalculateMoneyEarned(float height, float speed) {
        int amountEarned = (int) (height + speed / speedDecreasing);
        speedDecreasing += (int)(height / 10);

        Debug.Log("Earned: " + amountEarned);

        AddMoney(amountEarned);
    }

    #endregion
}
