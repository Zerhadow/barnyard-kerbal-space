using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuildState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GameBuildState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter() {
        base.Enter();

        Debug.Log("STATE: Game Build");

        //enable build controller
        _controller.buildController.gameObject.SetActive(true);

        // Activate canva elems
        _controller.UI.launchCanvas.SetActive(true);
        _controller.UI.partsShopCanvas.SetActive(true);
        _controller.UI.powerupPanel.SetTargetPosition(_controller.UI.powerupPanel.startOpen);
        //_controller.playerController.player.SetActive(true);

        //enable grid
        _controller.buildController.EnableGridMask(true);
        _controller.buildController.partParent.EnablePartGridBackgrounds(true);
    }

    public override void Update()
    {
        base.Update();

        //check for tap input
        if(Input.GetMouseButtonDown(0)) {
            // _stateMachine.ChangeState(_stateMachine.LobbyState);
        }
    }

    public override void Exit() {
        base.Exit();

        // deactivate canva elems
        _controller.UI.launchCanvas.SetActive(false);
        _controller.UI.partsShopCanvas.SetActive(false);

        //disable grid
        _controller.buildController.EnableGridMask(false);
        _controller.buildController.partParent.EnablePartGridBackgrounds(false);

        //follow camera
        _controller.cameraController.EnableDisableCameraFollow(true);

        //disable build controller
        _controller.buildController.gameObject.SetActive(false);

    }
}