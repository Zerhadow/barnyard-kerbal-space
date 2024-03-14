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

    public SimplifiedGrid SimplifiedGrid;

    [Header("Grid Helpers")]
    [SerializeField] private GameObject _grid;
    [SerializeField] private TextMeshProUGUI _coordDisplay;

    [Header("Debug")]
    public bool debugMode = false;

    private Camera _mainCamera;

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

        //always move selected part with mouse
        if (_selectedPart != null)
        {
            _selectedPart.transform.position = tileCenter;
        }
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
    [SerializeField] private RocketPart _selectedPart;
    [SerializeField] private Vector2 _previousPosition;

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

        if(_selectedPart == null)
        {
            //raycast check if selecting a tile
            RaycastHit2D hit = Utility.GetMouseHit2D();
            if (hit)
            {
                if (hit.collider.TryGetComponent(out RocketPart rocketPart))
                {
                    _previousPosition = rocketPart.transform.position;
                    _selectedPart = rocketPart;
                }
            }
            else
            {
                _selectedPart = null;
                _previousPosition = Vector2.zero;
            }
        }
        else
        {
            MouseUpAction(xy, tileCenter);
        }

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


        if(_selectedPart != null )
        {
            if (_selectedPart.isValidPlacement == true)
            {
                //can be dropped
                _selectedPart = null;
            }
            else
            {
                //shouldn't be dropped
                if (_previousPosition != Vector2.zero)
                    _selectedPart.transform.position = _previousPosition;
                else
                    RemovePart(_selectedPart); //destroy maybe

                _selectedPart = null;
            }

            _previousPosition = Vector2.zero;
        }

        RocketPart.OnPartMoved?.Invoke();
    }
    public void RightClickAction(Vector2Int xy, Vector2 tileCenter)
    {
        if(_selectedPart != null )
        {
            RemovePart(_selectedPart);
        }

        MouseUpAction(xy, tileCenter);
    }
    #endregion

    #region Rocket Part Actions
    private void RemovePart(RocketPart rocketPart)
    {
        /// TODO Decide how to handle removing rocket parts
        /// 

        partParent.RemovePartFromRocket(rocketPart);
    }
    public void SpawnPart(RocketPart partPrefab)
    {
        // if(partParent.RocketParts.Count == 0) { // check if it's the first part
        //     StartCoroutine(DelaySpawnPart(partPrefab));
        // } 

        if (partPrefab.partType == PartType.Character)
        {
            bool check = partParent.CheckIfRocketHasCharacter();
            if (!check)
            { // if true, don't allow playing part
                StartCoroutine(DelaySpawnPart(partPrefab));
            }
        }
        else
        {
            StartCoroutine(DelaySpawnPart(partPrefab));
        }
    }
    #endregion

    #region UI Stuff

    IEnumerator DelaySpawnPart(RocketPart partPrefab)
    {
        yield return new WaitForNextFrameUnit();

        if (_selectedPart == null)
        {
            RocketPart rocketPart = Instantiate(partPrefab, partParent.transform);
            _selectedPart = rocketPart;
            rocketPart.transform.position = new Vector3(0, -435, 0);
            partParent.TryAddPartToRocket(_selectedPart);
        }
    }
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
