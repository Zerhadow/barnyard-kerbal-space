using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSetupState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GameSetupState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter() {
        base.Enter();

        Debug.Log("STATE: Game Setup");

        // Disables everything on the canvas
        // Iterate through all child GameObjects
        /*foreach (Transform child in _controller.UI.canvas.transform)
        {
            // Set each child GameObject to inactive
            child.gameObject.SetActive(false);
        }*/

        _controller.UI.overlayCanvas.SetActive(false);
        _controller.UI.launchCanvas.SetActive(false);
        _controller.UI.partsShopCanvas.SetActive(false);

        // Activate canva elems
        // _controller.playerController.player.SetActive(false);

        // open canvas and play intro
        _controller.UI.videoCanvas.SetActive(true);
        _controller.videoController.PlayVideo("intro");
    }

    public override void Update()
    {
        base.Update();

        // //check for tap input
        // if(Input.GetMouseButtonDown(0)) {
        //     _stateMachine.ChangeState(_stateMachine.BuildState);
        // }
    }

    public override void Exit() {
        base.Exit();
    }
}