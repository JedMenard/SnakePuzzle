using SnakePuzzle.Classes;

internal class Program
{
    private static void Main(string[] args)
    {
        List<int> segments = new List<int> {
            2,
            2,
            3,
            4,
            3,
            3,
            2,
            2,
            3,
            3,
            2,
            4,
            2,
            2,
            3,
            3,
            4,
            4,
            3,
            2,
            2,
            2,
            4,
            4,
            2,
            4,
            2,
            3,
            4,
            3,
            2,
            2,
            2,
            2,
            3,
            2
        };

        /*
        List<int> segments = new List<int> { 2, 2 };
        */

        Puzzle puzzle = new Puzzle(segments, 4);

        List<DirectionEnum> solution = puzzle.FindSolution();

        foreach (DirectionEnum direction in solution)
        {
            Console.WriteLine(direction);
            Console.ReadLine();
        }
    }
}