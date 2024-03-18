using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    private BuildController _buildController;

    [Header("Properties")]
    public int puchaseCost = 10;

    [Header("Stats")]
    public int weight;
    public int thrust;
    public int durability;

    [Header("Dependencies")]
    [SerializeField] private GameObject _disabledImage;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _costText;

    private void Awake()
    {
        _buildController = FindObjectOfType<BuildController>();
    }
    private void Start()
    {
        if(_disabledImage != null)
            _disabledImage.SetActive(false);
    }
    private void Update()
    {
        if(_costText != null)
        {
            if(_costText.text != "$" + puchaseCost.ToString())
            {
                _costText.text = "$" + puchaseCost.ToString();
            }
        }
    }
    public void AddPowerup()
    {
        _buildController.partParent.powerupWeight += weight;
        _buildController.partParent.powerupThrust += thrust;
        _buildController.partParent.powerupDurability += durability;

        DisablePowerup();
    }
    public void DisablePowerup()
    {
        if(_disabledImage != null)
        {
            _disabledImage.SetActive(true);
        }
        if(_buyButton != null)
        {
            _buyButton.interactable = false;
        }
    }
}
