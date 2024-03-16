using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RocketParent : MonoBehaviour
{
    public List<RocketPart> RocketParts { get; private set; }
    public GameObject player;

    [Header("Physics Calculations")]
    public float charWeight;
    public int totalLoadMass;
    private int totalMass;
    private int totalThrust;
    public float fNet;
    
    private Vector3 originalPos;
    public Action OnRocketModified = delegate { };

    private void Awake()
    {
        //TODO If we add a save system will need to get list from save system
        RocketParts = new List<RocketPart>();
        originalPos = transform.position;
    }
    private void OnEnable()
    {
        RocketPart.OnPartChanged += CheckAttachedParts;
    }
    private void OnDisable()
    {
        RocketPart.OnPartChanged -= CheckAttachedParts;
    }

    #region Rocket Part 
    public void CheckAttachedParts()
    {
        Debug.Log("CheckAttachedParts()");

        /// start with character part
        /// change all adjacent to attached
        /// recursive loop it
        /// 

        //set all to false to start
        RocketPart.OnPartAttachmentReset?.Invoke();

        //store the character part
        RocketPart characterPart = null;
        foreach (var part in RocketParts)
        {
            if(part.partType == PartType.Character)
                characterPart = part;
        }

        //recursive loop to update all attached parts
        if(characterPart != null) 
        {
            //only check if character exists
            foreach (var part in characterPart.GetAdjacentParts())
            {
                part.SetAttachedToCharacter();
            }
        }
    }
    public void TryAddPartToRocket(RocketPart part)
    {
        RocketParts.Add(part);

        OnRocketModified?.Invoke();
    }
    public void RemovePartFromRocket(RocketPart part)
    {
        RocketParts.Remove(part);

        //restock
        part.partPanel.stock++;

        //destroy
        Destroy(part.gameObject);

        OnRocketModified?.Invoke();

        Debug.Log(RocketParts.Count);
        
    }
    public void ToggleBuildMode(bool isBuildMode)
    {
        foreach(var part in RocketParts)
        {
            part.isBuildMode = isBuildMode;
        }
    }
    
    #endregion

    #region Rocket stuff
    public void EnablePartGridBackgrounds(bool enable)
    {
        foreach (var part in RocketParts)
        {
            part.EnableGridBackground(enable);
        }
    }
    public void InitializeRocket()
    {
        if (RocketParts.Count > 0)
        {
            //add detached parts to list
            List<RocketPart> partsToRemove = new List<RocketPart>();
            foreach (RocketPart part in RocketParts)
            {
                if (part.isAttachedToCharacter == false)
                {
                    partsToRemove.Add(part);
                }
            }

            //remove detached parts
            foreach (RocketPart part in partsToRemove)
            {
                RemovePartFromRocket(part);
            }
        }
    }
    public bool CheckIfRocketHasCharacter()
    {
        bool hasCharacter = false;
        foreach (RocketPart part in RocketParts)
        {
            if (part.partType == PartType.Character)
            {
                hasCharacter = true;
            }
        }
        return hasCharacter;
    }
    #endregion

    private void SetCharacterWeight() {
        foreach (RocketPart part in RocketParts)
        {
            if (part.partType == PartType.Character)
            {
                charWeight = part.weight;
            }
        }
    }

    private float CalculatefGain() {
        foreach (RocketPart part in RocketParts)
        {
            totalLoadMass += part.weight;
            totalThrust += part.thrust;
        }

        float fBoosters = totalLoadMass * totalThrust;

        return fBoosters;
    }
    
    public float CalculateVelocity() { // F = ma - mg
        SetCharacterWeight(); // updates charWeight
        float fGrav = charWeight * 9.81f; // force of char
        float fBoosters = CalculatefGain();

        if(fBoosters == 0) { // player didnt add anything else
            fBoosters = fGrav + 10f;
        }

        fNet = fBoosters - fGrav;
        Debug.Log("fNet: " + fNet);

        return fNet;
    }

    public void ResetInfo() {
        charWeight = 0;
        totalLoadMass = 0;
        totalThrust = 0;
    }

    public void ResetLocation() { // after results screen, send parent obj back to 0,0,0
        transform.position = originalPos;
    }
}
