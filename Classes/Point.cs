namespace SnakePuzzle.Classes;

public class Point
{
    public int X;
    public int Y;
    public int Z;

    public Point(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public Point(Point other)
    {
        this.X = other.X;
        this.Y = other.Y;
        this.Z = other.Z;
    }

    /// <summary>
    /// Returns a new point representing the next point in the provided direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="steps"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Point GetNextPointInDirection(DirectionEnum direction, int steps = 1)
    {
        switch (direction)
        {
            case DirectionEnum.PositiveX:
                return new Point(this.X + steps, this.Y, this.Z);
            case DirectionEnum.NegativeX:
                return new Point(this.X - steps, this.Y, this.Z);
            case DirectionEnum.PositiveY:
                return new Point(this.X, this.Y + steps, this.Z);
            case DirectionEnum.NegativeY:
                return new Point(this.X, this.Y - steps, this.Z);
            case DirectionEnum.PositiveZ:
                return new Point(this.X, this.Y, this.Z + steps);
            case DirectionEnum.NegativeZ:
                return new Point(this.X, this.Y, this.Z - steps);
            default:
                throw new NotImplementedException($"Unexpected direction when getting next point: {direction}");
        }
    }

    #region Operator Overloads

    public override bool Equals(object? obj)
    {
        if (obj is Point other)
        {
            return this == other;
        }

        return false;
    }

    public static bool operator==(Point? first, Point? second)
    {
        if (first is null || second is null)
        {
            return first is null && second is null;
        }

        return first.X == second.X
            && first.Y == second.Y
            && first.Z == second.Z;
    }

    public static bool operator !=(Point? first, Point? second)
    {
        if (first is null || second is null)
        {
            return false;
        }

        return first.X != second.X
            && first.Y != second.Y
            && first.Z != second.Z;
    }

    public override string ToString()
    {
        return $"X: {this.X}, Y: {this.Y}, Z: {this.Z}";
    }

    public override int GetHashCode()
    {
        return this.ToString().GetHashCode();
    }

    #endregion
}
