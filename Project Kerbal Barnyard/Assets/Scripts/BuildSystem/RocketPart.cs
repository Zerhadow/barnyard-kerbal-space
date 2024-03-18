using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : MonoBehaviour
{
    private BuildController _bController;
    private RocketPartDebug _rocketPartDebug;

    [Header("Stats")]
    public int weight;
    public int thrust;
    public int durability;
    public PartType partType;
    public PartPanel partPanel;

    [Header("Grid Interaction")]
    [Tooltip("Determines if the part can be placed/added to the ship.")]
    public bool isNotOverlapping = true;
    public bool isAboveMinimum = true;
    public bool isAttachedToCharacter = false;
    public bool isNextToPart = false;
    public bool isBuildMode = true;
    [Space]
    [Tooltip("List of adjacent coordinates to check for other rocket parts.")]
    public List<Vector2Int> borderOffsets = new List<Vector2Int>();

    [Header("FX")]
    [SerializeField] private GameObject _vfxObject;

    public static Action OnPartChanged = delegate { };
    public static Action OnPartAttachmentReset = delegate { };

    #region Monobehavior
    private void Awake()
    {
        _bController = FindObjectOfType<BuildController>();
        _rocketPartDebug = GetComponent<RocketPartDebug>();

        if(partType == PartType.Character)
        {
            if(isAttachedToCharacter == false) isAttachedToCharacter = true;
        }
    }
    private void Start()
    {
        isNotOverlapping = true;
        //Debug.Log("[RocketPart] Start");
        OnPartChanged?.Invoke();

        DisableVFX();
        CheckIfAboveMinPosition(this, _bController.SimplifiedGrid.GetXY(transform.position));
    }
    private void OnEnable()
    {
        OnPartChanged += CheckIfNextToPart;
        BuildController.OnGridPositionChanged += CheckIfAboveMinPosition;
        OnPartAttachmentReset += ResetAttachment;

        PlayerController.OnRocketLaunched += EnableVFX;
        PlayerController.OnFlightOver += DisableVFX;
    }
    private void OnDisable()
    {
        OnPartChanged -= CheckIfNextToPart;
        BuildController.OnGridPositionChanged -= CheckIfAboveMinPosition;
        OnPartAttachmentReset -= ResetAttachment;

        PlayerController.OnRocketLaunched -= EnableVFX;
        PlayerController.OnFlightOver -= DisableVFX;
    }
    #endregion

    #region Custom Functions
    public void EnableVFX()
    {
        if(_vfxObject != null)
        {
            _vfxObject.SetActive(true);
        }
    }
    public void DisableVFX()
    {
        if (_vfxObject != null)
        {
            _vfxObject.SetActive(false);
        }
    }
    public void EnableGridBackground(bool enable)
    {
        _rocketPartDebug.gridImage.gameObject.SetActive(enable);
    }
    public void CheckIfNextToPart()
    {
        bool isAdjacent = false;
        foreach(var offset in borderOffsets)
        {
            //physics overlap checks surrounding tiles
            Collider2D collider = Physics2D.OverlapCircle((Vector2)transform.position + offset, 0.35f);
            if (collider != null)
            {
                isAdjacent = true;

                break;
            }
        }
        isNextToPart = isAdjacent;
    }
    public void CheckIfAboveMinPosition(RocketPart selectedPart, Vector2Int gridXYPosition)
    {
        if (selectedPart != this) return;

        bool aboveMinimum = true;
        foreach(var offset in borderOffsets)
        {
            Vector2Int adjustedXY = gridXYPosition + offset;

            if(adjustedXY.y + 1 < _bController.minHeightAllowed)
            {
                aboveMinimum = false;
            }
        }

        isAboveMinimum = aboveMinimum;
    }
    public void ResetAttachment()
    {
        if(partType != PartType.Character) isAttachedToCharacter = false;
    }
    public void SetAttachedToCharacter()
    {
        if(isAttachedToCharacter == false)
        {
            isAttachedToCharacter = true;

            foreach(RocketPart part in GetAdjacentParts())
            {
                part.SetAttachedToCharacter();
            }
        }
    }
    #region Not working pls ignore (for now)
    /*public void CheckIfAttachedToCharacter()
    {
        if(partType != PartType.Character && isAttachedToRocket == false)
        {
            List<RocketPart> adjacentParts = GetAdjacentParts();
            bool isAttached = false;

            foreach(var part in adjacentParts)
            {
                if(part.partType == PartType.Character || part.isAttachedToRocket == true)
                {
                    isAttached = true; 
                    break;
                }
            }

            isAttachedToRocket = isAttached;

            foreach(var part in adjacentParts)
            {
                if (isAttachedToRocket)
                    CheckIfAttachedToCharacter();
                else
                    CheckIfDetachedFromCharacter();
            }
                
        }
    }
    public void CheckIfDetachedFromCharacter()
    {

    }*/
    #endregion
    public List<RocketPart> GetAdjacentParts()
    {
        List<RocketPart> parts = new List<RocketPart>();

        foreach (var offset in borderOffsets)
        {
            //physics overlap checks surrounding tiles
            Collider2D collider = Physics2D.OverlapCircle((Vector2)transform.position + offset, 0.35f);
            if (collider != null)
            {
                //get rocket part
                RocketPart part = collider.GetComponent<RocketPart>();
                if(part != null)
                {
                    parts.Add(part);
                }
            }
        }

        return parts;
    }
    #endregion

    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isNotOverlapping = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isNotOverlapping = true;
    }
    #endregion

    private void OnDrawGizmos()
    {
        if(borderOffsets.Count > 0)
        {
            foreach(var offset in borderOffsets)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere((Vector2)transform.position + offset, 0.35f);
            }
        }
    }
}

public enum PartType { Character, Thruster, Body, Misc}

