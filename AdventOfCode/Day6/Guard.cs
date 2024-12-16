namespace Day6;

public class Guard(Position _position, Size _mapSize)
{
    private readonly VisitedPositions alreadyVisited = new(_mapSize);

    private Guard(Position position, Direction direction, Size mapSize) : this(position, mapSize)
    {
        CurrentDirection = direction;
    }

    public Direction CurrentDirection { get; private set; } = Direction.Up;

    public Position CurrentPosition { get; private set; } = _position;

    public void MoveAndVisit()
    {
        Move();
        MarkCurrentAsVisited();
    }


    public Guard Copy()
    {
        return new Guard(CurrentPosition with { }, CurrentDirection, _mapSize);
    }

    public void ChangeDirection()
    {
        CurrentDirection = CurrentDirection switch
        {
            Direction.Up => Direction.Right,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            Direction.Right => Direction.Down,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void Move()
    {
        CurrentPosition = CurrentPosition.ApplyTransition(CurrentDirection.GetTransition());
    }

    public Position NextPosition()
    {
        return CurrentPosition.ApplyTransition(CurrentDirection.GetTransition());
    }

    public bool HasAlreadyVisitedCurrent()
    {
        return alreadyVisited.HasBeenVisited(CurrentPosition) &&
               alreadyVisited.GetDirection(CurrentPosition) == CurrentDirection;
    }

    public bool HasVisited(Position position, Direction direction)
    {
        return alreadyVisited.HasBeenVisited(position) &&
               alreadyVisited.GetDirection(position) == direction;
    }

    public void MarkCurrentAsVisited()
    {
        alreadyVisited.MarkAsVisited(CurrentPosition, CurrentDirection);
    }
}