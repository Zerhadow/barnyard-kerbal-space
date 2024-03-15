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
    public int totalThrust;
    public float fNet;
    
    private Vector3 originalPos;
    public Action OnRocketModified = delegate { };

    private void Awake()
    {
        //TODO If we add a save system will need to get list from save system
        RocketParts = new List<RocketPart>();
        originalPos = transform.position;
    }


    #region Rocket Part 
    /// <summary>
    /// Will return false if RocketParts already contains the part.
    /// </summary>
    /// <param name="part"></param>
    /// <returns></returns>
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
    
    #endregion

    #region Rocket stuff
    public void EnablePartGridBackgrounds(bool enable)
    {
        foreach (var part in RocketParts)
        {
            part.EnableGridBackground(enable);
        }
    }
    public void ResetRocketTransform()
    {

    }
    public void InitializeRocket()
    {

        if (RocketParts.Count > 0)
        {
            /// loop through each and check if next to character
            /// start with character part

            RocketPart characterPart = null;

            foreach (RocketPart part in RocketParts)
            {
                if(part.partType == PartType.Character)
                    characterPart = part;
            }

            foreach (RocketPart part in characterPart.GetAdjacentParts())
            {
                part.isAttachedToRocket = true;
            }

            for (int i = 0; i < RocketParts.Count; i++)
            {
                RocketPart iPart = RocketParts[i];

                foreach (RocketPart part in RocketParts)
                {

                }
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

    public float CalculateVelocity() { // F = ma - mg
        float fGrav = charWeight * 9.81f; // force of char
        float fBoosters = totalLoadMass * totalThrust;

        fNet = fBoosters - fGrav;
        Debug.Log("fNet: " + fNet);

        return fNet;
    }

    public void ResetLocation() { // after results screen, send parent obj back to 0,0,0
        transform.position = originalPos;
    }
}
