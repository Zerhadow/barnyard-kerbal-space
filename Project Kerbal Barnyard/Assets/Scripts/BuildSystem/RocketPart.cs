using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : MonoBehaviour
{
    /*[Header("Status")]
    [SerializeField] private bool _isOccupied = false;*/

    [Header("Stats")]
    public int weight;
    public int thrust;
    public int durability;
    public PartType partType;

    [Header("Grid Interaction")]
    [Tooltip("Determines if the part can be placed/added to the ship.")]
    public bool isValidPlacement = true;
    public bool isAttachedToRocket = false;
    public bool isNextToPart = false;
    [Space]
    [Tooltip("List of adjacent coordinates to check for other rocket parts.")]
    public List<Vector2Int> borderOffsets = new List<Vector2Int>();

    private BuildController _bController;
    //private List<RocketPartSection> sections;

    public static Action OnPartMoved = delegate { };

    #region Monobehavior
    private void Awake()
    {
        _bController = FindObjectOfType<BuildController>();
    }
    private void Start()
    {
        isValidPlacement = true;
        Debug.Log("[RocketPart] Start");
    }
    private void OnEnable()
    {
        OnPartMoved += CheckIfNextToPart;
    }
    private void OnDisable()
    {
        OnPartMoved -= CheckIfNextToPart;
    }
    #endregion

    #region Custom Functions
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

