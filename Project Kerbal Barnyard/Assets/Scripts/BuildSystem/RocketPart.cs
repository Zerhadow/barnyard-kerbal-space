using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : MonoBehaviour
{

}
public class GridTile
{
    private GenericGrid<GridTile> grid;

    public int x { get; private set; }
    public int y { get; private set; }

    public bool isOccupied { get; private set; }

    public int _value;

    public GridTile(GenericGrid<GridTile> grid, int x, int y)
    {
        _value = 0;
        this.grid = grid;
        this.x = x;
        this.y = y;

        isOccupied = false;
    }
}
public class PartData
{
    private GenericGrid<PartData> grid;

    public int x { get; private set; }
    public int y { get; private set; }
    public bool isSelected { get; private set; }

    private enum States { Empty, Road, Building }
    private States currentState;

    public int _value;

    public PartData(GenericGrid<PartData> grid, int x, int y)
    {
        _value = 0;
        this.grid = grid;
        this.x = x;
        this.y = y;

        isSelected = false;
    }
    public Vector2Int GetXY()
    {
        return new Vector2Int(x, y);
    }
    public void Select()
    {
        isSelected = true;

        RefreshCurrentState();
        grid.TriggerGridObjectChanged(x, y);
    }
    public void Deselect()
    {
        isSelected = false;

        RefreshCurrentState();
        grid.TriggerGridObjectChanged(x, y);
    }
    public void MakeRoad()
    {
        _value = 2;
        RefreshCurrentState();
        grid.TriggerGridObjectChanged(x, y);
    }
    public void SetValue(int value)
    {
        _value = value;
        RefreshCurrentState();
        grid.TriggerGridObjectChanged(x, y);
    }
    public void AddValue()
    {
        _value++;
        if (_value > 2)
            _value = 0;

        RefreshCurrentState();
        grid.TriggerGridObjectChanged(x, y);
    }
    public void ResetValue()
    {
        _value = 0;

        RefreshCurrentState();
        grid.TriggerGridObjectChanged(x, y);
    }
    private void RefreshCurrentState()
    {
        currentState = (States)_value;
    }
    public override string ToString()
    {
        string ret = x + "," + y + "\n" + currentState.ToString();
        return ret;
    }
}
