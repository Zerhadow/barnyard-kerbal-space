
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

        Debug.Log("STATE: Game Lose");

        // Activate canva elems
        _controller.UI.resultsOverlay.SetActive(true);
    }

    public override void Update()
    {
        base.Update();

        // either click on screen or button
        if(Input.GetMouseButtonDown(0)) {
            // go back to build state
            _stateMachine.ChangeState(_stateMachine.BuildState);
        }
    }

    public override void Exit() {
        base.Exit();

        _controller.UI.resultsOverlay.SetActive(false);
    }
}