using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _currencyText;

    private void OnEnable()
    {
        CurrencyManager.OnCurrencyChanged += UpdateText;
    }
    private void OnDisable()
    {
        CurrencyManager.OnCurrencyChanged -= UpdateText;
    }

    private void UpdateText(int amount)
    {
        if(_currencyText != null)
        {
            _currencyText.text = "$" + amount.ToString();
        }
    }
}
