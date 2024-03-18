using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GameWinState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter() {
        base.Enter();

        Debug.Log("STATE: Game Win");

        // Activate canva elems
        _controller.UI.winOverlay.SetActive(true);
        _controller.UI.overlayCanvas.SetActive(false);

        float currPlayerPoss = _controller.playerController.GetCurrentHeight();
        _controller.UI.maxHeight1.text += " " + currPlayerPoss.ToString("F2");
        _controller.UI.maxSpd1.text += " " + _controller.playerController.shipInfo.fNet;
        _controller.UI.numParts1.text += " " + _controller.playerController.shipInfo.RocketParts.Count;
        _controller.UI.maxWeight1.text += " " + _controller.playerController.shipInfo.totalLoadMass + " banjos";
    
        // get character name
        string name = "win" + _controller.playerController.GetCharacterName();
        _controller.UI.videoCanvas.SetActive(true);
        _controller.videoController.PlayVideo(name);
        _controller.playerController.parentObj.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        // either click on screen or button
        if(Input.GetMouseButtonDown(0)) {
            // go to credit scene
            // SceneManager.LoadScene("Main Menu");
        }
    }

    public override void Exit() {
        base.Exit();

        _controller.UI.winOverlay.SetActive(false);

        // Reset canvas elems
        _controller.UI.maxHeight.text = "Height Reached:";
        _controller.UI.maxSpd.text = "Top Speed:";
        _controller.UI.numParts.text = "Number of Parts:";
        _controller.UI.maxWeight.text = "Weight:";

        _controller.playerController.shipInfo.ResetInfo();
        _controller.playerController.ResetVeloctity();

        _controller.UI.videoCanvas.SetActive(false);
    }
}