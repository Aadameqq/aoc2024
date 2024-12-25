namespace Day15;

public class Warehouse(List<char[]> _map)
{
    public const char Player = '@';
    public const char Wall = '#';
    public const char Box = 'O';
    public const char Empty = '.';
    private readonly List<char[]> map = _map;
    private readonly int sizeX = _map[0].Length;
    private readonly int sizeY = _map.Count;

    public char GetElementAtPosition(Position Position)
    {
        return map[Position.Y][Position.X];
    }

    public void SetElementAtPosition(Position Position, char value)
    {
        map[Position.Y][Position.X] = value;
    }

    public Position LocatePlayer()
    {
        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                var currPosition = new Position(x, y);
                if (GetElementAtPosition(currPosition) == Player)
                {
                    return currPosition;
                }
            }
        }

        throw new InvalidOperationException("Player not found on the map.");
    }

    public long CalculateCoordinatesSum()
    {
        var sum = 0L;

        const int yCoordinateMultiplier = 100;
        const int xCoordinateMultiplier = 1;

        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                if (GetElementAtPosition(new Position(x, y)) == Box)
                {
                    sum += y * yCoordinateMultiplier + x * xCoordinateMultiplier;
                }
            }
        }

        return sum;
    }

    public void PrintMap()
    {
        foreach (var row in map)
        {
            foreach (var col in row)
            {
                Console.Write(col);
            }

            Console.WriteLine();
        }
    }
}