namespace SnakePuzzle.Classes;

public class Puzzle
{
    /// <summary>
    /// List representing the base segment lengths of the puzzle.
    /// </summary>
    private List<int> segments;
    private int maxGridSize;

    public Puzzle(List<int> segments, int maxGridSize)
    {
        this.segments = segments;
        this.maxGridSize = maxGridSize;
    }

    /// <summary>
    /// Recursively tries all possible orientations to find the solution.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<DirectionEnum> FindSolution()
    {
        // Instantiate an empty grid and an empty move list.
        Grid<bool?> grid = new(this.maxGridSize);
        List<DirectionEnum> moveList = new List<DirectionEnum>();

        // Place the first piece.
        moveList.Add(DirectionEnum.PositiveX);
        Point? point = this.FillGridInDirection(grid,
            new Point(0, 0, 0),
            DirectionEnum.PositiveX,
            this.segments[0]);

        if (point == null)
        {
            // The first piece should never fail. If it does, throw an error.
            throw new ApplicationException("Impossible grid, no solution exists.");
        }

        // Begin the recursion
        List<DirectionEnum>? solution = this.FindSolution(moveList, grid, point);

        if (solution == null)
        {
            // No possible solution to this puzzle.
            // This shouldn't happen, so throw an error.
            throw new ApplicationException("Unsolveable puzzle");
        }

        return solution;
    }

    /// <summary>
    /// Determines what the next legal moves are,
    /// then recursively calls itself with each legal move.
    /// </summary>
    /// <param name="movesSoFar"></param>
    /// <param name="grid"></param>
    /// <param name="lastPlacedPoint"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private List<DirectionEnum>? FindSolution(List<DirectionEnum> movesSoFar, Grid<bool?> grid, Point lastPlacedPoint)
    {
        // Cache some stuff.
        int movesMade = movesSoFar.Count;
        DirectionEnum lastMoveMade = movesSoFar.Last();

        // Base case: We're at the end!
        if (movesMade == this.segments.Count)
        {
            return movesSoFar;
        }

        // Find the next segment.
        int segment = this.segments[movesMade];

        // Figure out which directions we can go from here.
        // Each step has to be orthogonal, so we can just get those directions.
        List<DirectionEnum> potentialMoves = lastMoveMade.GetOrthogonalDirections();

        // Recursively try each option.
        foreach (DirectionEnum direction in potentialMoves)
        {
            // Copy the grid for the next iteration.
            Grid<bool?> newGrid = new Grid<bool?>(grid);

            // The provided point should already have been placed.
            // Find the point one step in this direction from where we are.
            Point startingPoint = lastPlacedPoint.GetNextPointInDirection(direction);

            // Check if we can place the remaining pieces in that direction.
            Point? nextPoint = this.FillGridInDirection(newGrid, startingPoint, direction, segment - 1);

            // If nextPoint is null, that means this direction is invalid. Try the next one.
            if (nextPoint == null)
            {
                continue;
            }

            // This direction is valid. Pop it onto the move list, and check the next step.
            List<DirectionEnum> moves = new List<DirectionEnum>(movesSoFar);
            moves.Add(direction);
            List<DirectionEnum>? successfullMoves = this.FindSolution(moves, newGrid, nextPoint);

            // Success?
            if (successfullMoves != null)
            {
                // Success!
                return successfullMoves;
            }

            // Nope, try the next direction.
            continue;
        }

        // If we've gotten here, then there are no remaining valid moves.
        // Return null.
        return null;
    }

    /// <summary>
    /// Attempts to fill the provided grid,
    /// starting at the provided point,
    /// moving in the provided direction,
    /// as many steps as requested.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="startingPoint"></param>
    /// <param name="direction"></param>
    /// <param name="steps"></param>
    /// <returns></returns>
    private Point? FillGridInDirection(Grid<bool?> grid, Point startingPoint, DirectionEnum direction, int steps)
    {
        // Declare this outside of the loop so we can return it.
        Point? point = null;

        for (int i = 0; i < steps; i++)
        {
            // Determine where the new point is.
            point = startingPoint.GetNextPointInDirection(direction, i);

            // Check that the point is valid in the grid and that the point is not already full.
            if (!grid.PointIsValid(point) || grid.PointIsFilled(point))
            {
                return null;
            }

            // Fill the point.
            grid[point] = true;
        }

        // Return the last point that was filled.
        return point;
    }
}
