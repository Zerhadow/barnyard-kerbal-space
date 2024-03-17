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
        _controller.UI.overlayCanvas.SetActive(true);

        //reset durability events
        _controller.durabilityEvents.ResetEventCounter();
        _controller.playerController.SetGravityScale(0);

        // countdown
        _controller.audioController.PlayCountDownIntroHelper();

        // launch now happens in audio controller to sync up with music
        // starting countdown timer here
        _controller.countdownTimer.StartCountdown();
    }

    public override void Update()
    {
        base.Update();

        float currPlayerPoss = _controller.playerController.GetCurrentHeight();
        // have ship transform follow player
        _controller.playerController.MoveParts(currPlayerPoss);
        _controller.UI.heightTxt.text = "Height: " + currPlayerPoss.ToString("F2");
        _controller.UI.speedTxt.text = "Speed: " + _controller.playerController.GetCurrentSpeed().ToString("F2");

        // check if obj is falling
        if(_controller.playerController.Falling()) {
            Debug.Log("Obj is falling");
            // stop cam movement
            // trigger lose state
            _stateMachine.ChangeState(_stateMachine.LoseState);
        }

        if(currPlayerPoss >= 1250f) {
            _controller.audioController.PlayVariant();
        }

        if(currPlayerPoss >= _controller.playerController.winHeight) {
            _stateMachine.ChangeState(_stateMachine.WinState);
        }

        // change tracks when player hits certain height
        // _controller.audioController.playTheme1.Stop();
        // _controller.audioController.playTheme2.Play();

    }

    public override void Exit() {
        base.Exit();

        //_controller.UI.playParentObj.SetActive(true);
        _controller.playerController.SetGravityScale(0);

        _controller.audioController.musicSource.Stop();

        //trigger flight over event
        PlayerController.OnFlightOver?.Invoke();
    }
}