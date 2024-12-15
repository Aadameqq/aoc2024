namespace Day6;

public class Guard(Position _position)
{
    private Guard(Position position, Direction direction) : this(position)
    {
        CurrentDirection = direction;
    }

    public Direction CurrentDirection { get; private set; } = Direction.Up;

    public Position CurrentPosition { get; private set; } = _position;


    public Guard Copy()
    {
        return new Guard(CurrentPosition with { }, CurrentDirection);
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
}