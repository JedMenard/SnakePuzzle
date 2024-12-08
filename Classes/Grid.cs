namespace SnakePuzzle.Classes;

public class Grid<T>
{
    private Dictionary<Point, T?> grid;
    public int MaxGridSize;

    private int GetMaxX() => this.grid.Keys.MaxBy(key => key.X)?.X ?? 0;
    private int GetMinX() => this.grid.Keys.MinBy(key => key.X)?.X ?? 0;
    private int GetMaxY() => this.grid.Keys.MaxBy(key => key.Y)?.Y ?? 0;
    private int GetMinY() => this.grid.Keys.MinBy(key => key.Y)?.Y ?? 0;
    private int GetMaxZ() => this.grid.Keys.MaxBy(key => key.Z)?.Z ?? 0;
    private int GetMinZ() => this.grid.Keys.MinBy(key => key.Z)?.Z ?? 0;

    public Grid(int maxGridSize) {
        this.grid = new();
        this.MaxGridSize = maxGridSize;
    }

    public Grid(Grid<T> other)
    {
        this.grid = new();
        this.MaxGridSize = other.MaxGridSize;

        foreach ((Point point, T? value) in other.grid)
        {
            this.grid[new Point(point)] = value;
        }
    }

    public bool PointIsFilled(Point point)
    {
        if (!this.PointIsValid(point))
        {
            // The requested point is invalid.
            throw new ArgumentException($"Invalid point: {point.ToString()}");
        }

        return this.grid.GetValueOrDefault(point) == null ? false : true;
    }

    public bool PointIsValid(Point point)
    {
        // Find the distance from each max and min value in the grid.
        int distanceFromMaxX = Math.Abs(this.GetMaxX() - point.X);
        int distanceFromMinX = Math.Abs(this.GetMinX() - point.X);
        int distanceFromMaxY = Math.Abs(this.GetMaxY() - point.Y);
        int distanceFromMinY = Math.Abs(this.GetMinY() - point.Y);
        int distanceFromMaxZ = Math.Abs(this.GetMaxZ() - point.Z);
        int distanceFromMinZ = Math.Abs(this.GetMinZ() - point.Z);

        // Verify that the new point is not too far from any existing point.
        return distanceFromMaxX < this.MaxGridSize
            && distanceFromMinX < this.MaxGridSize
            && distanceFromMaxY < this.MaxGridSize
            && distanceFromMinY < this.MaxGridSize
            && distanceFromMaxZ < this.MaxGridSize
            && distanceFromMinZ < this.MaxGridSize;
    }

    public T? this[Point point]
    {
        get => this.grid[point];
        set => this.grid[point] = value;
    }
}
