using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartPanel : MonoBehaviour
{
    [Header("Properties")]
    public int stock = 1;
    public int cost = 10;
    public RocketPart partPrefab;

    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _stockText;
    [SerializeField] private TextMeshProUGUI _costText;

    private int _savedStock;
    private int _savedCost;

    private void Awake()
    {
        
    }
    private void Update()
    {
        #region Update Text (non fancy way)
        if(_savedStock != stock)
        {
            UpdateText();
            _savedStock = stock;
        }
        if(_savedCost != cost)
        {
            UpdateText();
            _savedCost = cost;
        }
        #endregion
    }
    private void UpdateText()
    {
        if(_stockText != null) _stockText.text = stock.ToString("00");
        if(_costText != null) _costText.text = cost.ToString("000");
    }
}
