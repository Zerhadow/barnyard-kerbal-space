using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PartPanel : MonoBehaviour
{
    private BuildController _buildController;

    [Header("Properties")]
    public int stock = 1;
    public int unlockCost = 10;
    public RocketPart partPrefab;
    public bool isUnlocked = false;

    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _stockText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private GameObject _costImage;
    [SerializeField] private GameObject _lockedImage;

    private int _savedStock;
    private int _savedCost;

    private void Awake()
    {
        _buildController = FindObjectOfType<BuildController>();
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
        if(_costImage != null)
        {
            if(isUnlocked == true && _costImage.activeInHierarchy == true)
                _costImage.SetActive(false);
            if(isUnlocked == false && _costImage.activeInHierarchy == false)
                _costImage.SetActive(true);
        }
        #endregion
    }

    #region Public Functions
    public void TrySpawnPart()
    {
        if(isUnlocked == false)
        {
            //unlock if can afford it
            if(CurrencyManager.money >= unlockCost)
            {
                CurrencyManager.Instance.RemoveMoney(unlockCost);
                isUnlocked = true;
            }
        }
        else
        {
            if(partPrefab != null)
            {
                //spawn part if in stock
                if (stock >= 1)
                {
                    bool spawned = _buildController.SpawnPart(partPrefab, this);
                    if (spawned)
                    {
                        Debug.Log("Spawn part");
                        stock--;
                        if (stock < 0) stock = 0;
                    }
                }
                else
                {
                    Debug.Log("Part out of stock");
                }
            }
            else
            {
                Debug.LogError("Missing part prefab");
            }
            
        }
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
