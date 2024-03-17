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
    public PowerupPanel powerupPanel;

    [Header("Play Dependencies")]
    //public GameObject playParentObj;
    public TMP_Text heightTxt;
    public TMP_Text speedTxt;

    [Header("Results Dependencies")]
    public TMP_Text maxHeight;
    public TMP_Text maxSpd;
    public TMP_Text numParts;
    public TMP_Text maxWeight;
    
    [Header("Win Dependencies")]
    public TMP_Text maxHeight1;
    public TMP_Text maxSpd1;
    public TMP_Text numParts1;
    public TMP_Text maxWeight1;

    [Header("Cursor Dependencies")]
    public CursorManager cursorManager;

    [Header("Tutorial")]
    public TutorialPanel tutorialPanel;

    private void Awake() {
        _stateMachine = GetComponentInParent<GameFSM>();
    }

    public void LaunchBtn() {
        _stateMachine.ChangeState(_stateMachine.PlayState);
    }
}
