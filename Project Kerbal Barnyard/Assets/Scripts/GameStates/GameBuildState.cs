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
        _controller.UI.buildParentObj.SetActive(true);
        _controller.playerController.player.SetActive(true);
        //enable grid
        _controller.buildController.EnableGridMask(true);
    }

    public override void Update()
    {
        base.Update();

        //check for tap input
        if(Input.GetMouseButtonDown(0)) {
            // _stateMachine.ChangeState(_stateMachine.LobbyState);
        }
        
        // after certain amount of time, trigger intro anim
    }

    public override void Exit() {
        base.Exit();

        // deactivate canva elems
        _controller.UI.buildParentObj.SetActive(false);
        //disable grid
        _controller.buildController.EnableGridMask(false);
        //follow camera
        _controller.cameraController.EnableDisableCameraFollow(true);

        //disable build controller
        _controller.buildController.gameObject.SetActive(false);

    }
}