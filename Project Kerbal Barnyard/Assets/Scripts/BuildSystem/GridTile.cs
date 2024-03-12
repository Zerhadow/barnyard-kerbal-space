public class GridTile
{
    private SimplifiedGrid grid;

    public int x { get; private set; }
    public int y { get; private set; }

    public bool isOccupied { get; private set; }

    public int _value;

    public GridTile(SimplifiedGrid grid, int x, int y)
    {
        _value = 0;
        this.grid = grid;
        this.x = x;
        this.y = y;

        isOccupied = false;
    }

    public void ToggleIsOccupied(bool isOccupied)
    {
        this.isOccupied = isOccupied;
        grid.OnGridTileChanged?.Invoke(this);
    }
    public override string ToString()
    {
        //return base.ToString();
        return isOccupied.ToString();
    }
}
