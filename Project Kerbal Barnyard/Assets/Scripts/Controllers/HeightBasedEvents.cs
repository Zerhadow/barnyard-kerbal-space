using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightBasedEvents : MonoBehaviour
{
    private GameController _controller;

    [Header("Base")]
    [Tooltip("The heights the player must reach to trigger the events.")]
    public List<float> eventHeights = new List<float>();

    /// <summary>
    /// Passes the event ID (order of the events).
    /// </summary>
    public Action<int> OnDurabilityEvent;

    public int eventCounter { get; private set; } = 0;

    private void Awake()
    {
        _controller = FindObjectOfType<GameController>();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void Start()
    {
        ResetEventCounter();
    }
    private void Update()
    {
        if(eventHeights.Count > 0)
        {
            if(_controller.playerController.GetCurrentHeight() >= eventHeights[eventCounter])
            {
                OnDurabilityEvent?.Invoke(eventCounter);
            }
        }
    }
    public void ResetEventCounter()
    {
        eventCounter = 0;
    }
    public virtual void EventTriggered(int eventID)
    {

    }
}
