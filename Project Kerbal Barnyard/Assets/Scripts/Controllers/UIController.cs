using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject canvas;
    private GameFSM _stateMachine;

    [Header("Build Dependencies")]
    public GameObject buildParentObj;
    [Header("Play Dependencies")]
    public GameObject playParentObj;

    private void Awake() {
        _stateMachine = GetComponentInParent<GameFSM>();
    }

    public void LaunchBtn() {
        _stateMachine.ChangeState(_stateMachine.PlayState);
    }
}
