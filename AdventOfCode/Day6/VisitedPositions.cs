namespace Day6;

public class VisitedPositions(int mapSizeX, int mapSizeY)
{
    private readonly Direction[,] visited = new Direction [mapSizeX, mapSizeY];

    public bool HasBeenVisited(Position position)
    {
        return visited[position.X, position.Y] != Direction.None;
    }

    public void MarkAsVisited(Position position, Direction direction)
    {
        visited[position.X, position.Y] = direction;
    }

    public Direction GetDirection(Position position)
    {
        if (!HasBeenVisited(position))
            throw new ArgumentException(
                "The specified position must have been visited to determine its visit direction");

        return visited[position.X, position.Y];
    }
}