namespace Day15;

public class Warehouse
{
    public const char Robot = '@';
    public const char Wall = '#';
    public const char Box = 'O';
    public const char Empty = '.';
    protected List<char[]> map;

    public Warehouse(List<char[]> _map)
    {
        map = _map;
    }

    protected Warehouse()
    {
    }

    private int sizeX => map[0].Length;
    private int sizeY => map.Count;

    public char GetElementAtPosition(Position Position)
    {
        return map[Position.Y][Position.X];
    }

    public void SetElementAtPosition(Position Position, char value)
    {
        map[Position.Y][Position.X] = value;
    }

    public Position LocateRobot()
    {
        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                var currPosition = new Position(x, y);
                if (GetElementAtPosition(currPosition) == Robot)
                {
                    return currPosition;
                }
            }
        }

        throw new InvalidOperationException("Player not found on the map.");
    }

    public long CalculateCoordinatesSum(char boxChar = Box)
    {
        var sum = 0L;

        const int yCoordinateMultiplier = 100;
        const int xCoordinateMultiplier = 1;

        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                if (GetElementAtPosition(new Position(x, y)) == boxChar)
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