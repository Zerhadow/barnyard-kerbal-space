using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    [Header("Text Options")]
    [SerializeField] private string _overrideText;
    [SerializeField] private bool _doOverrideText = false;
    [Space]
    [SerializeField] private float _endingScale = 2.0f;
    [SerializeField] private float _startingScale = 1.0f;
    [SerializeField] private float _lerpSpeed = 1.0f;

    private float _sizeProgress;
    private bool _isVisible = false;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        EnableText(false);
    }
    private void Update()
    {
        if (_isVisible)
        {
            //lerp and resize
            _sizeProgress = Mathf.Lerp(_sizeProgress, _endingScale, _lerpSpeed * Time.deltaTime);
            SetTextScale(_sizeProgress);

            bool increment = _endingScale > _startingScale ? true : false;

            if(increment)
            {
                if (_sizeProgress >= _endingScale)
                {
                    EnableText(false);
                }
            }
            else
            {
                if (_sizeProgress <= _endingScale)
                {
                    EnableText(false);
                }
            }
            
        }
    }
    public void Popup(string text)
    {
        if(_text != null)
        {
            //show text
            EnableText(_text);

            //reset size
            _sizeProgress = _startingScale;
            SetTextScale(_sizeProgress);

            //populate Text
            if (_doOverrideText)
            {
                _text.text = _overrideText;
            }
            else
            {
                _text.text = text;
            }
        }
        else
        {
            Debug.LogError("Missing text reference.");
        }
    }
    public void EnableText(bool enable)
    {
        if(_text != null)
        {
            _text.gameObject.SetActive(enable);
            _isVisible = enable;
        }
    }
    private void SetTextScale(float scale)
    {
        if(_text != null)
        {
            _text.transform.localScale = Vector3.one * scale;
        }
    }
}
