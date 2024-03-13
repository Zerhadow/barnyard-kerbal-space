using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketParent : MonoBehaviour
{
    public List<RocketPart> RocketParts { get; private set; }
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
    public bool TryAddPartToRocket(RocketPart part)
    {
        if (RocketParts.Contains(part))
        {
            Debug.LogWarning("Part is already on the rocket.");
            return false;
        }
        else
        {
            RocketParts.Add(part);

            OnRocketModified?.Invoke();

            return true;
        }

    }
    public void RemovePartFromRocket(RocketPart part)
    {
        if(RocketParts.Contains(part))
        {
            RocketParts.Remove(part);

            OnRocketModified?.Invoke();
        }
        else
        {
            Debug.LogWarning("Rocket does not contain this part.");
        }

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
