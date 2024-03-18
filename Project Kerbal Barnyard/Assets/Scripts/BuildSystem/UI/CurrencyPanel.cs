using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyPanel : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _currencyText;
    [Space]
    [SerializeField] private Button _addMoney;
    [SerializeField] private Button _removeMoney;

    private void OnEnable()
    {
        CurrencyManager.OnCurrencyChanged += UpdateText;
    }
    private void OnDisable()
    {
        CurrencyManager.OnCurrencyChanged -= UpdateText;
    }
    private void Start()
    {
        if (!CurrencyManager.Instance.DebugEnabled)
        {
            if(_addMoney != null) _addMoney.gameObject.SetActive(false);
            if(_removeMoney != null) _removeMoney.gameObject.SetActive(false);
        }
    }

    private void UpdateText(int amount)
    {
        if(_currencyText != null)
        {
            _currencyText.text = "$" + amount.ToString();
        }
    }
}
