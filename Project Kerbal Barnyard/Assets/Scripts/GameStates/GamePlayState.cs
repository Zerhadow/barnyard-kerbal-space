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
        _controller.playerController.Launch();
    }

    public override void Update()
    {
        base.Update();

        // check if obj is falling
        if(_controller.playerController.Falling()) {
            Debug.Log("Obj is falling");
        }
    }

    public override void Exit() {
        base.Exit();

        _controller.UI.playParentObj.SetActive(true);
        _controller.playerController.SetGravityScale(0);
    }
}