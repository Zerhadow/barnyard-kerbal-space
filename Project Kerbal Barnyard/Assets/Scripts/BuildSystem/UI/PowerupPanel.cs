using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PowerupPanel : MonoBehaviour
{
    [Header("Options")]
    public bool startOpen = false;
    [Space]
    public float offsetAmount = 100f;
    public float lerpSpeed = 5f;

    private RectTransform _panelTransform;

    private Vector2 openPosition = new Vector2(0, 100);
    private Vector2 closePosition;

    private Vector2 _targetPosition;

    private void Awake()
    {
        _panelTransform = GetComponent<RectTransform>();
        float width = _panelTransform.rect.width;
        float height = _panelTransform.rect.height;
        closePosition = new Vector2(-width, 100);
    }
    private void Update()
    {
        #region Lerp Movement
        if (_panelTransform.anchoredPosition != _targetPosition)
        {
            if(Vector2.Distance(_panelTransform.anchoredPosition, _targetPosition) > 5f)
            {
                _panelTransform.anchoredPosition = Vector2.Lerp(_panelTransform.anchoredPosition, _targetPosition, lerpSpeed * Time.deltaTime);
            }
            else
            {
                _panelTransform.anchoredPosition = _targetPosition;
            }
        }
        #endregion
    }

    public void SetTargetPosition(bool open)
    {
        Debug.Log("SetTargetPosition()");

        if (open)
            _targetPosition = openPosition;
        else
            _targetPosition = closePosition;
    }
    public void TogglePosition()
    {
        if (_targetPosition == openPosition)
            _targetPosition = closePosition;
        else
            _targetPosition = openPosition;
    }
}
