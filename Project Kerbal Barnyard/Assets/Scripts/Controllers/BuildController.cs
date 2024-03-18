using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
// using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    private GameController _gameController;

    [Header("Dependencies")]
    public RocketParent partParent;

    [Header("Grid Dimensions")]
    public int width = 10;
    public int height = 10;
    public float cellSize;
    public Vector3 startPosition;
    public int minHeightAllowed = 0;

    public SimplifiedGrid SimplifiedGrid;

    [Header("Grid Helpers")]
    [SerializeField] private GameObject _grid;
    [SerializeField] private TextMeshProUGUI _coordDisplay;

    [Header("Debug")]
    public bool debugMode = false;

    public static Action<RocketPart> OnSelectedPartChanged = delegate { };
    public static Action<RocketPart, Vector2Int> OnGridPositionChanged = delegate { };
    public static Action OnBuildModeStarted = delegate { };

    private Camera _mainCamera;
    private Vector2Int _savedVectorInt;

    private void Awake()
    {
        _gameController = GetComponentInParent<GameController>();
        _mainCamera = Camera.main;
    }
    private void Start()
    {
        //setup grid
        SimplifiedGrid = new SimplifiedGrid(width, height, cellSize, startPosition);

        //disable stuff
        EnableGridMask(false); //grid
        gameObject.SetActive(false); //controller
        _gameController.UI.partsShopCanvas.SetActive(false); // parts shop UI

        //trigger actions
        OnSelectedPartChanged?.Invoke(selectedPart);
    }
    private void Update()
    {
        #region Position Info
        //breakdown the information related to the mouse position and the grid
        Vector2 rawWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int gridXYPosition = SimplifiedGrid.GetXY(rawWorldPosition);
        Vector2 tilePosition = SimplifiedGrid.GetWorldPosition(gridXYPosition);
        Vector2 tileCenter = tilePosition + new Vector2(cellSize * 0.5f, cellSize * 0.5f);
        #endregion

        #region Tile changed action
        if(_savedVectorInt != gridXYPosition)
        {
            _savedVectorInt = gridXYPosition;
            if(selectedPart != null)
            {
                OnGridPositionChanged?.Invoke(selectedPart, gridXYPosition);
                selectedPart.transform.position = tileCenter;
            }
        }
        #endregion

        #region Debug
        if (debugMode)
        {
            if (_coordDisplay != null)
            {
                if(_coordDisplay.transform.position != (Vector3)tileCenter) _coordDisplay.transform.position = tileCenter;
                if(_coordDisplay.text != gridXYPosition.ToString()) _coordDisplay.text = gridXYPosition.ToString();
            }
        }
        #endregion

        #region Input
        //check for input
        if (Input.GetMouseButtonDown(0))
        {
            MouseDownAction(gridXYPosition, tileCenter);
        }
        if (Input.GetMouseButtonDown(1))
        {
            RightClickAction(gridXYPosition, tileCenter);
        }
        #endregion

        //moved to Tile changed Action (line 75)
        /*//always move selected part with mouse
        if (_selectedPart != null)
        {
            _selectedPart.transform.position = tileCenter;
        }*/
    }

    #region Grid Actions
    public void OccupyPartTiles()
    {

    }
    public void UnoccupyPartTiles()
    {

    }
    #endregion



    #region Click Actions
    //[SerializeField] private Transform _selectedPart;
    public RocketPart selectedPart { get; private set; }
    private Vector2 _previousPosition;

    public void MouseDownAction(Vector2Int xy, Vector2 tileCenter)
    {
        #region Debug Only
        //add gridtile to dictionary
        /*if (!SimplifiedGrid.GridTiles.ContainsKey(xy))
        {
            GridTile gridTile = new GridTile(SimplifiedGrid, xy.x, xy.y);
            gridTile.ToggleIsOccupied(true);

            SimplifiedGrid.GridTiles.Add(xy, gridTile);
        }
        //or toggle isOccupied
        if (SimplifiedGrid.GridTiles.ContainsKey(xy))
        {
            SimplifiedGrid.GridTiles[xy].ToggleIsOccupied(!SimplifiedGrid.GridTiles[xy].isOccupied);
        }*/
        #endregion

        /// if no piece, pickup piece
        ///

        if(selectedPart == null)
        {
            //raycast check if selecting a tile
            RaycastHit2D hit = Utility.GetMouseHit2D();
            if (hit)
            {
                if (hit.collider.TryGetComponent(out RocketPart rocketPart))
                {
                    _previousPosition = rocketPart.transform.position;
                    selectedPart = rocketPart;
                    OnSelectedPartChanged?.Invoke(selectedPart);
                }
            }
            else
            {
                selectedPart = null;
                OnSelectedPartChanged?.Invoke(selectedPart);
                _previousPosition = Vector2.zero;
            }
        }
        else
        {
            MouseUpAction(xy, tileCenter);
        }

        RocketPart.OnPartChanged?.Invoke();
    }
    public void MouseHoldAction(Vector2Int xy, Vector2 tileCenter)
    {
        /// if piece is picked up, move it around with mouse based on grid position
        ///

        /// not needed anymore
    }
    public void MouseUpAction(Vector2Int xy, Vector2 tileCenter)
    {
        /// if piece is picked up, check if it can be placed and place it on grid
        ///     only if not on top of other piece and if next to placed piece
        /// 


        if(selectedPart != null )
        {
            if (selectedPart.isNotOverlapping == true && selectedPart.isAboveMinimum == true)
            {
                //can be dropped
                selectedPart = null;
                OnSelectedPartChanged?.Invoke(selectedPart);
            }
            else
            {
                //shouldn't be dropped
                if (_previousPosition != Vector2.zero)
                    selectedPart.transform.position = _previousPosition;
                else
                    RemovePart(selectedPart); //destroy maybe

                selectedPart = null;
                OnSelectedPartChanged?.Invoke(selectedPart);
            }

            _previousPosition = Vector2.zero;
        }

        //RocketPart.OnPartChanged?.Invoke();
    }
    public void RightClickAction(Vector2Int xy, Vector2 tileCenter)
    {
        if(selectedPart != null )
        {
            RemovePart(selectedPart);
        }

        MouseUpAction(xy, tileCenter);

        RocketPart.OnPartChanged?.Invoke();
    }
    #endregion

    #region Rocket Part Actions
    public void ResetRocket()
    {
        //remove rocket parts
        partParent.RemoveAllParts();
    }
    private void RemovePart(RocketPart rocketPart)
    {
        /// TODO Decide how to handle removing rocket parts
        /// 

        partParent.RemovePartFromRocket(rocketPart);
    }
    public bool SpawnPart(RocketPart partPrefab, PartPanel partPanel)
    {

        if (partPrefab.partType == PartType.Character)
        {
            bool check = partParent.CheckIfRocketHasCharacter();
            if (!check)
            { 
                // if true, don't allow playing part
                StartCoroutine(DelaySpawnPart(partPrefab, partPanel));
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            StartCoroutine(DelaySpawnPart(partPrefab, partPanel));
            return true;
        }
    }
    IEnumerator DelaySpawnPart(RocketPart partPrefab, PartPanel partPanel)
    {
        yield return new WaitForNextFrameUnit();

        if (selectedPart == null)
        {
            RocketPart rocketPart = Instantiate(partPrefab, partParent.transform);
            rocketPart.transform.position = new Vector3(0, -435, 0);
            rocketPart.partPanel = partPanel;
            selectedPart = rocketPart;
            OnSelectedPartChanged?.Invoke(selectedPart);

            partParent.TryAddPartToRocket(selectedPart);
        }
    }
    #endregion

    #region UI Stuff
    public void EnableGridMask(bool enable)
    {
        if (_grid != null) _grid.SetActive(enable);
    }
    #endregion




    /*private void OnDrawGizmos()
    {
        if(SimplifiedGrid != null)
        {
            foreach (var tileData in SimplifiedGrid.GridTiles)
            {
                if (tileData.Value.isOccupied)
                {
                    //get center pos
                    Vector2 worldPos = SimplifiedGrid.GetWorldPosition(tileData.Key);
                    Vector2 center = worldPos + (Vector2.one * cellSize * 0.5f);

                    //draw gizmo
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(center, Vector3.one * cellSize * 0.5f);
                }
            }
        } 
    }*/
}
