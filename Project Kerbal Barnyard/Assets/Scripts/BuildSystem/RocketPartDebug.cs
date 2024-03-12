using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPartDebug : MonoBehaviour
{
    private RocketPart rocketPart;

    [SerializeField] private SpriteRenderer _partImage;
    [SerializeField] private Color _validColor;
    [SerializeField] private Color _invalidColor;

    private void Awake()
    {
        rocketPart = GetComponent<RocketPart>();
    }
    private void Update()
    {
        if(rocketPart == null) rocketPart = GetComponent<RocketPart>();

        if(rocketPart.isValidPlacement == true && _partImage.color != _validColor)
            _partImage.color = _validColor;
        if(rocketPart.isValidPlacement == false && _partImage.color != _invalidColor)
            _partImage.color = _invalidColor;
    }
}
