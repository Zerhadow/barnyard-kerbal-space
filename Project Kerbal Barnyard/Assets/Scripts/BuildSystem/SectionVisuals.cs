using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class SectionVisuals : MonoBehaviour
{
    private RocketPartSection _section;

    private bool _savedConnectUp;
    private bool _savedConnectRight;
    private bool _savedConnectDown;
    private bool _savedConnectLeft;

    [Header("References")]
    [SerializeField] private GameObject _upVisual;
    [SerializeField] private GameObject _rightVisual;
    [SerializeField] private GameObject _downVisual;
    [SerializeField] private GameObject _leftVisual;

    private void Update()
    {
        if(_section == null) _section = GetComponent<RocketPartSection>();

        #region Trigger Updates
        /*if (_savedConnectUp != _section.connectUp)
        {
            RefreshVisuals();
            _savedConnectUp = _section.connectUp;
        }
        if(_savedConnectRight != _section.connectRight)
        {
            RefreshVisuals();
            _savedConnectRight = _section.connectRight;
        }
        if(_savedConnectDown != _section.connectDown)
        {
            RefreshVisuals();
            _savedConnectDown = _section.connectDown;
        }
        if(_savedConnectLeft != _section.connectLeft)
        {
            RefreshVisuals();
            _savedConnectLeft = _section.connectLeft;
        }*/
        #endregion
    }

    private void RefreshVisuals()
    {
        /*if(_upVisual != null) _upVisual.SetActive(_section.connectUp);
        if(_rightVisual != null) _rightVisual.SetActive(_section.connectRight);
        if(_downVisual != null) _downVisual.SetActive(_section.connectDown);
        if(_leftVisual != null) _leftVisual.SetActive(_section.connectLeft);*/
    }
}
