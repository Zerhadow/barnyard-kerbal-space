using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPartSection : MonoBehaviour
{
    [Header("Connectors")]
    public bool connectUp;
    public bool connectRight;
    public bool connectDown;
    public bool connectLeft;

    public Vector2Int SectionOffset { get; private set; }
    public RocketPart ParentPart {  get; private set; }

    [Header("Status")]
    [SerializeField]
    private bool _isConnected = false;

    private bool _isOccupied = false;

    //private List<Collider2D> connectors = new List<Collider2D>();


    private void Awake()
    {
        //get the parent rocket part component
        if(ParentPart == null) ParentPart = GetComponentInParent<RocketPart>();
        //save the position of the section relative to parent
        SectionOffset = new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.y);
        //populate connectors list
        //connectors = new List<Collider2D>(GetComponentsInChildren<Collider2D>());
    }

    public void SetConnectedStatus(bool connected)
    {
        _isConnected = connected;
    }
    public bool GetOccupiedStatus(SimplifiedGrid grid)
    {
        Vector2Int xy = grid.GetXY(transform.position);

        if (grid.GridTiles.ContainsKey(xy))
        {
            return grid.GridTiles[xy].isOccupied;
        }
        else
        {
            return false;
        }
    }
    public bool GetIsValidSection()
    {
        /// check if this part can be placed
        ///     check if connected to another part
        ///     check if not overlapping with another part
        /// if valid => update parent to show it can be placed

        if(_isConnected == false && _isOccupied == true)
            return false;
        else
            return true;
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        //called by all child collider objects

        _isConnected = true;
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isConnected = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isConnected = false;
    }
}
