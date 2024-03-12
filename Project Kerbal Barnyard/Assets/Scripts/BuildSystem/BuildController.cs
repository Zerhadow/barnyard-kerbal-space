using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
// using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    private GameController _gameController;

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
    public bool debug = false;

    private Camera _mainCamera;

    private void Awake()
    {
        _gameController = GetComponentInParent<GameController>();
        _mainCamera = Camera.main;
    }
    private void Start()
    {
        EnableGridMask(false);

        //disable build controller
        gameObject.SetActive(false);
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
        if (debug)
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
        if (Input.GetMouseButton(0))
        {
            MouseHoldAction(gridXYPosition, tileCenter);
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUpAction(gridXYPosition, tileCenter);
        }
        #endregion
    }

    

    #region Grid Actions
    public void OccupyPartTiles()
    {

    }
    public void UnoccupyPartTiles()
    {

    }
    #endregion


    //[SerializeField] private Transform _selectedPart;
    [SerializeField] private RocketPart _selectedPart;
    [SerializeField] private Vector2 _previousPosition;

    #region Click Actions
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

        //raycast check if selecting a tile
        RaycastHit2D hit = Utility.GetMouseHit2D();
        if(hit != null)
        {
            RocketPart rocketPart = hit.collider.GetComponent<RocketPart>();
            if (rocketPart != null)
            {
                _previousPosition = rocketPart.transform.position;
                _selectedPart = rocketPart;
            }
        }
        

    }
    public void MouseHoldAction(Vector2Int xy, Vector2 tileCenter)
    {
        /// if piece is picked up, move it around with mouse based on grid position
        ///

        if(_selectedPart != null )
        {
            _selectedPart.transform.position = tileCenter;
        }
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
                _selectedPart.transform.position = _previousPosition;
                _selectedPart = null;
            }
        }
    }
    #endregion


    public void EnableGridMask(bool enable)
    {
        if(_grid != null) _grid.SetActive(enable);
    }


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
