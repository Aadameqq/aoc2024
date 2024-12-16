namespace Day6;

public class Map(Size size)
{
    private readonly bool[,] obstructions = new bool [size.X, size.Y];

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
        return x == 0 || x == size.X - 1 || y == 0 || y == size.Y - 1;
    }

    public bool HasObstruction(Position position)
    {
        return obstructions[position.X, position.Y];
    }
}