
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoseState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private float maxHeight, maxSpd;

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
        maxHeight = currPlayerPoss;
        _controller.UI.maxHeight.text += " " + maxHeight.ToString("F2");
        maxSpd = _controller.playerController.shipInfo.fNet;
        _controller.UI.maxSpd.text += " " + maxSpd;
        _controller.UI.numParts.text += " " + _controller.playerController.shipInfo.RocketParts.Count;
        _controller.UI.maxWeight.text += " " + _controller.playerController.shipInfo.totalLoadMass + " banjos";
    
        _controller.audioController.PlayMusic("Result Sound Music");
        
        _controller.videoController.PlayVideo("intro");

        // get character name
        string name = "lose" + _controller.playerController.GetCharacterName();
        _controller.UI.videoCanvas.SetActive(true);
        _controller.videoController.PlayVideo(name);
        _controller.playerController.parentObj.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
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

        _controller.currencyManager.CalculateMoneyEarned(maxHeight, maxSpd);

        _controller.UI.videoCanvas.SetActive(false);
        _controller.playerController.parentObj.SetActive(true);
    }
}