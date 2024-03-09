using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[ExecuteAlways]
public class DebugGridDisplay : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool _debugEnabled = true;

    BuildController _controller;

    private int _savedWidth;
    private int _savedHeight;
    private float _savedCellSize;
    private Vector3 _savedStartPosition;

    private void Update()
    {
        
        if(_controller == null) _controller = GetComponent<BuildController>();

        if (_controller.SimplifiedGrid == null)
        {
            _controller.SimplifiedGrid = new SimplifiedGrid(_controller.width, _controller.height, _controller.cellSize, _controller.startPosition);
            Debug.Log("Constructing Grid");
        }

        #region Debug Only
        if (_savedWidth != _controller.width)
        {
            _savedWidth = _controller.width;
            ReconstructGrid();
        }
        if (_savedHeight != _controller.height)
        {
            _savedHeight = _controller.height;
            ReconstructGrid();
        }
        if (_savedCellSize != _controller.cellSize)
        {
            _savedCellSize = _controller.cellSize;
            ReconstructGrid();
        }
        if (_savedStartPosition != _controller.startPosition)
        {
            _savedStartPosition = _controller.startPosition;
            ReconstructGrid();
        }
        #endregion

        if (_controller.SimplifiedGrid != null)
        {
            if(_debugEnabled) _controller.SimplifiedGrid.RefreshGridDisplay();
        }

    }

    private void ReconstructGrid()
    {
        if (_controller.SimplifiedGrid != null)
        {
            _controller.SimplifiedGrid.ResizeGrid(_controller.width, _controller.height, _controller.cellSize, _controller.startPosition);
        }

        if(_debugEnabled) Debug.LogWarning("ReconstructingGrid");
    }
}
