using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartInfo : MonoBehaviour
{
    private BuildController _buildController;

    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _weightText;
    [SerializeField] private TextMeshProUGUI _thrustText;
    [SerializeField] private TextMeshProUGUI _durabilityText;
    [SerializeField] private Transform _statPanel;

    [Header("Part Info")]
    public PartPanel partPanel;
    public RocketPart rocketPart;

    private bool _followPanel = false;

    private void Awake()
    {
        _buildController = FindObjectOfType<BuildController>();
    }
    private void Start()
    {
        if(_statPanel != null)
        {
            _statPanel.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        PartPanel.OnPanelHovered += ShowStats;
        PartPanel.OnPanelExit += HideStats;

        BuildController.OnSelectedPartChanged += CheckForSelected;
    }
    private void OnDisable()
    {
        PartPanel.OnPanelHovered -= ShowStats;
        PartPanel.OnPanelExit -= HideStats;
    }
    private void Update()
    {
        if(_statPanel != null)
        {
            if(_statPanel.gameObject.activeInHierarchy == true)
            {
                if(transform.position != partPanel.transform.position)
                {
                    transform.position = partPanel.transform.position;
                }
            }
        }
    }
    
    public void CheckForSelected(RocketPart rocketPart)
    {
        if(rocketPart != null)
        {
            HideStats(partPanel);
        }
    }
    public void ShowStats(PartPanel panel)
    {
        //do not show stats if not null
        if (_buildController.selectedPart != null) return;

        //save panel and rocket part
        partPanel = panel;
        rocketPart = panel.partPrefab;

        //move to panel position
        transform.position = partPanel.transform.position;

        //show stats if hidden
        if(_statPanel != null)
        {
            _statPanel.gameObject.SetActive(true);
        }
    }
    public void HideStats(PartPanel panel)
    {
        //reset panel and rocket part
        if(panel == partPanel)
        {
            partPanel = null;
            rocketPart = null;
        }

        //hide stats
        if (_statPanel != null)
        {
            _statPanel.gameObject.SetActive(false);
        }
    }
}
