using System;
using System.Collections;
using System.Collections.Generic;
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

    public Action OnRocketModified = delegate { };

    private void Awake()
    {
        //TODO If we add a save system will need to get list from save system
        RocketParts = new List<RocketPart>();
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
    public bool CheckIfRocketHasCharacter()
    {
        bool hasCharacter = false;
        foreach (RocketPart part in RocketParts)
        {
            if(part.partType == PartType.Character)
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

        float fNet = fBoosters - fGrav;
        Debug.Log("fNet: " + fNet);

        return fNet;
    }
}
