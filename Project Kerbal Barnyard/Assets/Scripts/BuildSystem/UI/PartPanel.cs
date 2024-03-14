using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartPanel : MonoBehaviour
{
    [Header("Properties")]
    public int stock = 1;
    public int unlockCost = 10;
    public RocketPart partPrefab;
    public bool isUnlocked = false;

    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _stockText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private GameObject _lockedImage;

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
        if(_savedCost != unlockCost)
        {
            UpdateText();
            _savedCost = unlockCost;
        }
        #endregion

        #region UnlockedDisplay
        if(_lockedImage != null)
        {
            if(isUnlocked == true && _lockedImage.activeInHierarchy == true)
                _lockedImage.SetActive(false);
            if(isUnlocked == false && _lockedImage.activeInHierarchy == false)
                _lockedImage.SetActive(true);
        }
        #endregion
    }

    #region Public Functions
    public void TryUnlock()
    {
        if(isUnlocked == false)
        {
            if(CurrencyManager.money >= unlockCost)
            {
                CurrencyManager.Instance.RemoveMoney(unlockCost);
                isUnlocked = true;
            }
        }
    }
    public void TrySpawnPart()
    {

    }
    #endregion

    #region Private visual updates
    private void UpdateText()
    {
        if(_stockText != null) _stockText.text = stock.ToString("00");
        if(_costText != null) _costText.text = unlockCost.ToString("000");
    }
    #endregion
}
