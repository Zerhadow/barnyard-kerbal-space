using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private Rigidbody2D rigidBody2D;

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
        rigidBody2D = _controller.player.GetComponent<Rigidbody2D>();
        rigidBody2D.gravityScale = _controller.gravityScale;
        MovePlayer(); // make obj go up
    }

    public override void Update()
    {
        base.Update();

        // check if obj is falling
        if(Falling()) {
            Debug.Log("Obj is falling");
        }
    }

    public override void Exit() {
        base.Exit();

        _controller.UI.playParentObj.SetActive(true);
        rigidBody2D.gravityScale = 0;
    }

    public void MovePlayer() {
        rigidBody2D.AddForce(Vector2.up * _controller.forceMagnitude);
    }

    private bool Falling() {
        if(rigidBody2D.velocity.y < 0f) { return true; }
        else { return false; }
    }
}