using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject overlayCanvas;
    private GameFSM _stateMachine;

    [Header("Build Dependencies")]
    public GameObject buildParentObj;
    [Header("Play Dependencies")]
    public GameObject playParentObj;
    public TMP_Text heightTxt;
    public TMP_Text speedTxt;

    private void Awake() {
        _stateMachine = GetComponentInParent<GameFSM>();
    }

    public void LaunchBtn() {
        _stateMachine.ChangeState(_stateMachine.PlayState);
    }
}
