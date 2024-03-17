
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoseState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GameLoseState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter() {
        base.Enter();

        Debug.Log("STATE: Game Lose");

        // Activate canva elems
        _controller.UI.resultsOverlay.SetActive(true);
        _controller.UI.overlayCanvas.SetActive(false);

        float currPlayerPoss = _controller.playerController.GetCurrentHeight();
        _controller.UI.maxHeight.text += " " + currPlayerPoss.ToString("F2");
        _controller.UI.maxSpd.text += " " + _controller.playerController.shipInfo.fNet;
        _controller.UI.numParts.text += " " + _controller.playerController.shipInfo.RocketParts.Count;
        _controller.UI.maxWeight.text += " " + _controller.playerController.shipInfo.totalLoadMass + " banjos";
    
        _controller.audioController.PlayMusic("Result Sound Music");
    }

    public override void Update()
    {
        base.Update();

        // either click on screen or button
        if(Input.GetMouseButtonDown(0)) {
            // go back to build state
            _stateMachine.ChangeState(_stateMachine.BuildState);
        }
    }

    public override void Exit() {
        base.Exit();

        _controller.UI.resultsOverlay.SetActive(false);
        _controller.playerController.shipInfo.ResetLocation();
        
        // Reset canvas elems
        _controller.UI.maxHeight.text = "Height Reached:";
        _controller.UI.maxSpd.text = "Top Speed:";
        _controller.UI.numParts.text = "Number of Parts:";
        _controller.UI.maxWeight.text = "Weight:";

        _controller.playerController.shipInfo.ResetInfo();
        _controller.playerController.ResetVeloctity();

        // make sure no music is playing before next try
        _controller.audioController.musicSource.Stop();
    }
}