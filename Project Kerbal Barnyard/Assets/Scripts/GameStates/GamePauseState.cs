using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GamePauseState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Update() {
        base.Update();

        // Watch for key press to un pause
        if(Input.GetKeyDown(KeyCode.Escape)) {
            // Check previous state
            _stateMachine.ChangeStateToPrevious();
        }
    }

    public override void Exit() {
        base.Exit();
    }
}
