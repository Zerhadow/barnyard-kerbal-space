using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPartDebug : MonoBehaviour
{
    private RocketPart rocketPart;

    public SpriteRenderer gridImage;
    [SerializeField] private Color _validColor;
    [SerializeField] private Color _invalidColor;
    [SerializeField] private Color _adjacentColor;
    [SerializeField] private Color _gridColor;
    [Space]
    [SerializeField] private Sprite _buildSprite;
    [SerializeField] private Sprite _playSprite;

    private void Awake()
    {
        rocketPart = GetComponent<RocketPart>();
    }
    private void Update()
    {
        if(rocketPart == null) rocketPart = GetComponent<RocketPart>();

        if(rocketPart != null) 
        {
            if (rocketPart.isBuildMode)
            {
                if(gridImage != null && gridImage.sprite != _buildSprite)
                    gridImage.sprite = _buildSprite;

                if (rocketPart.isValidPlacement == true)
                {
                    if (rocketPart.isAttachedToCharacter == true && gridImage.color != _adjacentColor)
                        gridImage.color = _adjacentColor;
                    if (rocketPart.isAttachedToCharacter == false && gridImage.color != _validColor)
                        gridImage.color = _validColor;
                }

                //if(rocketPart.isValidPlacement == true && _partImage.color != _validColor) _partImage.color = _validColor;
                if (rocketPart.isValidPlacement == false && gridImage.color != _invalidColor)
                    gridImage.color = _invalidColor;
            }
            else
            {
                if(gridImage != null && gridImage.color != _gridColor)
                    gridImage.color = _gridColor;
                if(gridImage != null && gridImage.sprite != _playSprite)
                    gridImage.sprite = _playSprite;
            }
        }
    }
}
