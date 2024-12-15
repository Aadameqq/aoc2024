namespace Day6;

public class Map(int sizeX, int sizeY)
{
    private readonly bool[,] obstructions = new bool [sizeX, sizeY];
    private readonly Direction[,] visitDirection = new Direction [sizeX, sizeY];
    private readonly bool[,] visited = new bool [sizeX, sizeY];
    public int TotalVisited { get; private set; }

    public void AddObstruction(Position position)
    {
        obstructions[position.X, position.Y] = true;
    }

    public void RemoveObstruction(Position position)
    {
        obstructions[position.X, position.Y] = false;
    }

    public bool IsPositionOnBorder(Position position)
    {
        var (x, y) = (x: position.X, y: position.Y);
        return x == 0 || x == sizeX - 1 || y == 0 || y == sizeY - 1;
    }

    public void Visit(Position position, Direction direction)
    {
        if (!IsVisited(position)) TotalVisited++;
        visited[position.X, position.Y] = true;
        visitDirection[position.X, position.Y] = direction;
    }

    public Direction GetVisitDirection(Position position)
    {
        if (!IsVisited(position))
            throw new ArgumentException(
                "The specified position must have been visited to determine its visit direction");

        return visitDirection[position.X, position.Y];
    }

    public bool HasObstruction(Position position)
    {
        return obstructions[position.X, position.Y];
    }

    public bool IsVisited(Position position)
    {
        return visited[position.X, position.Y];
    }
}