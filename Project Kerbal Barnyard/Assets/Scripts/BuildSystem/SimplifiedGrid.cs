using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SimplifiedGrid
{
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;

    private Dictionary<Vector2Int, GridTile> gridTiles;

    public Action<GridTile> OnGridTileChanged = delegate { };
    public Action OnGridDimensionsChanged = delegate { };

    public SimplifiedGrid(int width, int height, float cellSize, Vector3 originPosition)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        gridTiles = new Dictionary<Vector2Int, GridTile>();

        RefreshGridDisplay();
    }

    #region Helpers
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0) * _cellSize + _originPosition;
    }
    public Vector2Int GetXY(Vector3 worldPosition/*out int x, out int y*/)
    {
        Vector2Int coords = new Vector2Int();
        coords.x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        coords.y = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
        return coords;
    }
    #endregion

    #region ModifyGrid
    public void ResizeGrid(int width, int height, float cellSize, Vector3 originPosition)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;

        RefreshGridDisplay();
    }
    #endregion

    #region Debug/Display
    public void RefreshGridDisplay()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //draw grid
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.red, 0.1f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 0.1f);
            }
        }

        //draw borders
        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.red, 0.1f);
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.red, 0.1f);
    }
    #endregion




}
