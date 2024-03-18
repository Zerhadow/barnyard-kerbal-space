using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BuildController _buildController;

    [Header("Properties")]
    public int stock = 1;
    public int unlockCost = 10;
    public RocketPart partPrefab;
    public bool isUnlocked = false;

    [Header("Dependencies")]
    [SerializeField] private Button _spawnButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _stockText;
    [SerializeField] private TextMeshProUGUI _costText;
    [Space]
    [SerializeField] private GameObject _unlockImage;
    [SerializeField] private GameObject _buyText;
    [SerializeField] private GameObject _lockedImage;

    private int _savedStock;
    private int _savedCost;

    public static Action<PartPanel> OnPanelHovered = delegate { };
    public static Action<PartPanel> OnPanelExit = delegate { };

    public static Action<PartPanel> OnFailedPurchace = delegate { };

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
        if(_spawnButton != null)
        {
            if (isUnlocked == true && _spawnButton.interactable == false)
                _spawnButton.interactable = true;
            if (isUnlocked == false && _spawnButton.interactable == true)
                _spawnButton.interactable = false;
        }
        if(_buyButton != null)
        {
            if(partPrefab.partType == PartType.Character && _buyButton.gameObject.activeInHierarchy == true)
                _buyButton.gameObject.SetActive(false);
        }
        if (_lockedImage != null)
        {
            if(isUnlocked == true && _lockedImage.activeInHierarchy == true)
                _lockedImage.SetActive(false);
            if(isUnlocked == false && _lockedImage.activeInHierarchy == false)
                _lockedImage.SetActive(true);
        }
        if(_unlockImage != null)
        {
            if(isUnlocked == true && _unlockImage.activeInHierarchy == true)
            {
                _unlockImage.SetActive(false);
                _buyText.SetActive(true);
            }
            if (isUnlocked == false && _unlockImage.activeInHierarchy == false)
            {
                _unlockImage.SetActive(true);
                _buyText.SetActive(false);
            }
        }
        #endregion
    }

    #region Public Functions
    public void TryUnlockOrBuy()
    {
        if (isUnlocked == false)
        {
            //unlock if can afford it
            if (CurrencyManager.money >= unlockCost)
            {
                CurrencyManager.RemoveMoney(unlockCost);
                isUnlocked = true;
            }
            else
            {
                OnFailedPurchace?.Invoke(this);
            }
        }
        else
        {
            //buy more stock if can afford it
            if(CurrencyManager.money >= unlockCost)
            {
                CurrencyManager.RemoveMoney(unlockCost);
                stock++;
                int newCost = (int)(unlockCost * CurrencyManager.StockMultiplier);
                unlockCost += newCost;
            }
            else
            {
                OnFailedPurchace?.Invoke(this);
            }
        }
    }
    public void TrySpawnPart()
    {
        if(isUnlocked == true)
        {
            if (partPrefab != null)
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
        if(_costText != null) _costText.text = "$" + unlockCost.ToString("000");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPanelHovered?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPanelExit?.Invoke(this);
    }
    #endregion
}
