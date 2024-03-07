using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    private GameController _controller;

    
    // state instances
    public GameSetupState SetupState { get; private set; }
    public GamePlayState PlayState { get; private set; }
    public GameBuildState BuildState { get; private set; }
    public GameWinState WinState { get; private set; }
    public GameLoseState LoseState { get; private set; }

    private void Awake() {
        _controller = GetComponent<GameController>();

        // create states
        SetupState = new GameSetupState(this, _controller);
        PlayState = new GamePlayState(this, _controller);
        BuildState = new GameBuildState(this, _controller);
        WinState = new GameWinState(this, _controller);
        LoseState = new GameLoseState(this, _controller);
    }

    private void Start() {
        ChangeState(SetupState);
    }
}
