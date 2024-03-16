using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _selectedCursor;
    [SerializeField] private GameObject _deletePrompt;

    private Camera _mainCamera;
    private void OnEnable()
    {
        BuildController.OnSelectedPartChanged += UpdateCursor;
    }
    private void OnDisable()
    {
        BuildController.OnSelectedPartChanged -= UpdateCursor;
    }
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        _container.transform.position = Input.mousePosition;
    }

    public void UpdateCursor(RocketPart part)
    {
        if(part != null)
        {
            //use selected cursor
            Cursor.visible = false;
            _selectedCursor.SetActive(true);
            _deletePrompt.SetActive(true);
        }
        else
        {
            //use default cursor
            Cursor.visible = true;
            _selectedCursor.SetActive(false);
            _deletePrompt.SetActive(false);
        }
    }
    public void DefaultCursor()
    {

    }
}
