namespace Day15;

public class Transition(int X, int Y)
{
    public Transition(Position p1, Position p2) : this(p1.X - p2.X, p1.Y - p2.Y)
    {
    }

    public Position TransitionPosition(Position position)
    {
        return new Position(position.X + X, position.Y + Y);
    }

    public bool IsHorizontal()
    {
        return Y == 0;
    }
}