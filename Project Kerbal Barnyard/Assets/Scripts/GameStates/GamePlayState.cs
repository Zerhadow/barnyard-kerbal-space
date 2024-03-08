using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GamePlayState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter() {
        base.Enter();

        Debug.Log("STATE: Game Play");

        // Activate canva elems
        _controller.UI.playParentObj.SetActive(true);
        _controller.UI.overlayCanvas.SetActive(true);
        _controller.playerController.Launch();
    }

    public override void Update()
    {
        base.Update();

        float currPlayerPoss = _controller.playerController.GetCurrentHeight(_controller.playerController.player.transform.position.y);
        _controller.UI.heightTxt.text = "Height: " + currPlayerPoss.ToString("F2");
        _controller.UI.speedTxt.text = "Speed: " + _controller.playerController.GetCurrentSpeed().ToString("F2");

        // check if obj is falling
        if(_controller.playerController.Falling()) {
            Debug.Log("Obj is falling");
            // stop cam movement
            // trigger lose state
            _stateMachine.ChangeState(_stateMachine.LoseState);
        }

        if(currPlayerPoss >= _controller.playerController.winHeight) {
            _stateMachine.ChangeState(_stateMachine.WinState);
        }
    }

    public override void Exit() {
        base.Exit();

        _controller.UI.playParentObj.SetActive(true);
        _controller.playerController.SetGravityScale(0);
    }
}