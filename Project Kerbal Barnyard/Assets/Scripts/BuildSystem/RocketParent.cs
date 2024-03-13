using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketParent : MonoBehaviour
{
    public List<RocketPart> RocketParts { get; private set; }

    private void Awake()
    {
        //TODO If we add a save system will need to get list from save system
        RocketParts = new List<RocketPart>();
    }

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
            return true;
        }
    }
    public void RemovePartFromRocket(RocketPart part)
    {
        if(RocketParts.Contains(part))
        {
            RocketParts.Remove(part);
        }
        else
        {
            Debug.LogWarning("Rocket does not contain this part.");
        }
    }
}
