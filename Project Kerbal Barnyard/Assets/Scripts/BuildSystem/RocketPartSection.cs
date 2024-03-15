using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPartSection : MonoBehaviour
{
    /*[Header("Connectors")]
    public bool connectUp;
    public bool connectRight;
    public bool connectDown;
    public bool connectLeft;*/

    public Vector2Int SectionOffset { get; private set; }
    public RocketPart ParentPart {  get; private set; }

    //[Header("Status")]
    private bool _isOccupied = false;
    
    //[SerializeField] private bool _isOccupied = false;

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
    /*public bool GetOccupiedStatus(SimplifiedGrid grid)
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
    }*/
    public bool GetOccupiedStatus()
    {
        return _isOccupied;
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        _isOccupied = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isOccupied = false;
    }*/
}
