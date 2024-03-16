using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityEvents : HeightBasedEvents
{
    [Header("Durabiliy Settings")]
    [Tooltip("Required amount of durability to pass each durability event.")]
    public List<int> durabilityRequirements = new List<int>();

    private void Start()
    {
        if(durabilityRequirements.Count < eventHeights.Count)
        {
            Debug.LogError("[DurabilityEvents] Durability requirements missing.");
        }
    }
    public override void EventTriggered(int eventID)
    {
        base.EventTriggered(eventID);

        if(durabilityRequirements.Count > 0)
        {
            if (_controller.buildController.partParent.GetTotalDurability() > durabilityRequirements[eventID])
            {
                RocketPassesEvent();
            }
            else
            {
                RocketFailsEvent();
            }
        }
    }
    private void RocketPassesEvent()
    {
        Debug.Log("Passed durability check");
        //rocket continues normally
    }
    private void RocketFailsEvent()
    {
        Debug.Log("Failed durability check. Doubling GravityScale");
        //rocket speed is halved
        _controller.playerController.SetGravityScale(_controller.playerController.GetGravityScale() * 2f);
    }
}
