using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : MonoBehaviour
{
    /*[Header("Status")]
    [SerializeField] private bool _isOccupied = false;*/

    [Header("Stats")]
    public int weight;
    public int thrust;
    public int durability;
    [Space]
    [Tooltip("Determines if the part can be placed/added to the ship.")]
    public bool isValidPlacement = true;
    public bool isAttachedToRocket = false;

    private BuildController _bController;
    //private List<RocketPartSection> sections;

    public Action OnPartMoved;

    private void Awake()
    {
        _bController = FindObjectOfType<BuildController>();

        /*if(sections == null || sections.Count == 0)
        {
            sections = new List<RocketPartSection>(GetComponentsInChildren<RocketPartSection>());
        }*/
    }
    private void Start()
    {
        isValidPlacement = true;
    }
    private void OnEnable()
    {
        //OnPartMoved += CheckStatus;
    }
    private void OnDisable()
    {
        //OnPartMoved -= CheckStatus;
    }

    //call this everytime part moves position
    /*public void CheckStatus()
    {
        /// first reset isValid 
        /// next check sections
        ///     if any return false then cant place
        ///     

        isValidPlacement = true;

        foreach (var section in sections)
        {
            //isValidPlacement = section.GetIsValidSection();
            if(section.GetOccupiedStatus() == true) isValidPlacement = false;
            //if(section.GetOccupiedStatus(_bController.SimplifiedGrid) == true) isValidPlacement = false;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isValidPlacement = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isValidPlacement = true;
    }
}

