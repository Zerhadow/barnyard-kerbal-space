using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : MonoBehaviour
{
    private RocketPartDebug _rocketPartDebug;

    [Header("Stats")]
    public int weight;
    public int thrust;
    public int durability;
    public PartType partType;
    public PartPanel partPanel;

    [Header("Grid Interaction")]
    [Tooltip("Determines if the part can be placed/added to the ship.")]
    public bool isValidPlacement = true;
    public bool isAttachedToCharacter = false;
    public bool isNextToPart = false;
    public bool isBuildMode = true;
    [Space]
    [Tooltip("List of adjacent coordinates to check for other rocket parts.")]
    public List<Vector2Int> borderOffsets = new List<Vector2Int>();

    private BuildController _bController;
    //private List<RocketPartSection> sections;

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
        isValidPlacement = true;
        //Debug.Log("[RocketPart] Start");
        OnPartChanged?.Invoke();
    }
    private void OnEnable()
    {
        OnPartChanged += CheckIfNextToPart;
        OnPartAttachmentReset += ResetAttachment;

        PlayerController.OnRocketLaunched += EnableVFX;
        BuildController.OnBuildModeStarted += DisableVFX;
    }
    private void OnDisable()
    {
        OnPartChanged -= CheckIfNextToPart;
        OnPartAttachmentReset -= ResetAttachment;

        PlayerController.OnRocketLaunched -= EnableVFX;
        BuildController.OnBuildModeStarted -= DisableVFX;
    }
    #endregion

    #region Custom Functions
    public void EnableVFX()
    {

    }
    public void DisableVFX()
    {

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
        isValidPlacement = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isValidPlacement = true;
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

