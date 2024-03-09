using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderKeywordFilter;
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

    private void Awake()
    {
        _gameController = GetComponentInParent<GameController>();
    }
    private void Start()
    {
        EnableGridMask(false);
    }
    private void Update()
    {

    }

    public void EnableGridMask(bool enable)
    {
        if(_grid != null) _grid.SetActive(enable);
    }
    
}
