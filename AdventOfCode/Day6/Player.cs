namespace Day6;

public class Player(Position _position)
{
    private Direction direction = Direction.Up;

    private Player(Position position, Direction _direction) : this(position)
    {
        direction = _direction;
    }

    public Position CurrentPosition { get; private set; } = _position;


    public Player Copy()
    {
        return new Player(CurrentPosition, direction);
    }

    public void ChangeDirection()
    {
        direction = direction switch
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
        CurrentPosition = CurrentPosition.ApplyTransition(direction.GetTransition());
    }

    public Position NextPosition()
    {
        return CurrentPosition.ApplyTransition(direction.GetTransition());
    }
}