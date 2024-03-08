using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public override void Update()
    {
        base.Update();

        // either click on screen or button
        if(Input.GetMouseButtonDown(0)) {
            // _stateMachine.ChangeState(_stateMachine.BuildState);
            // scene change
        }
    }

    public override void Exit() {
        base.Exit();
    }
}