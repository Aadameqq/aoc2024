namespace Day6;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public static class DirectionExtensions
{
    public static Transition GetTransition(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Transition(0, -1),
            Direction.Down => new Transition(0, 1),
            Direction.Left => new Transition(-1, 0),
            Direction.Right => new Transition(1, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}