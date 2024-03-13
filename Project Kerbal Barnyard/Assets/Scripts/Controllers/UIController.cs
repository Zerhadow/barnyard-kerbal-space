using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private GameFSM _stateMachine;
    [Header("Canvas Dependencies")]
    //public GameObject canvas;
    public GameObject overlayCanvas;
    public List<GameObject> canvasObj = new List<GameObject>();
    public GameObject winOverlay;
    public GameObject resultsOverlay;

    [Header("Build Dependencies")]
    public GameObject launchCanvas;
    public GameObject partsShopCanvas;

    [Header("Play Dependencies")]
    //public GameObject playParentObj;
    public TMP_Text heightTxt;
    public TMP_Text speedTxt;

    private void Awake() {
        _stateMachine = GetComponentInParent<GameFSM>();
    }

    public void LaunchBtn() {
        _stateMachine.ChangeState(_stateMachine.PlayState);
    }
}
