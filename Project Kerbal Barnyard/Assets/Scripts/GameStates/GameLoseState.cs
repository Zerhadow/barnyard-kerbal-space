
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

        Debug.Log("STATE: Game Win");

        // Activate canva elems
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit() {
        base.Exit();
    }
}