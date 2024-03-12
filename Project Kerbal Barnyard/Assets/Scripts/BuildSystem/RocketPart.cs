using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : MonoBehaviour
{
    private BuildController _bController;

    public List<RocketPartSection> sections;

    public bool isValidPlacement;

    private void Awake()
    {
        _bController = FindObjectOfType<BuildController>();

        if(sections == null || sections.Count == 0)
        {
            sections = new List<RocketPartSection>(GetComponentsInChildren<RocketPartSection>());
        }
    }

    //call this everytime part moves position
    public bool CheckIfValidPlacement()
    {
        /// first reset isValid 
        /// next check sections
        ///     if any return false then cant place
        ///     

        isValidPlacement = true;

        foreach (var section in sections)
        {
            //isValidPlacement = section.GetIsValidSection();
            if(section.GetConnectedStatus() == false)
                isValidPlacement = false;
            if(section.GetOccupiedStatus(_bController.SimplifiedGrid) == true)
                isValidPlacement = false;
        }

        return isValidPlacement;
    }
}

