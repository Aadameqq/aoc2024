namespace Day15;

/*
 * { '^', new Transition(0, -1) },
   { '>', new Transition(1, 0) },
   { '<', new Transition(-1, 0) },
   { 'v', new Transition(0, 1) }
 */

public class Transition(int X, int Y)
{
    public static Transition BaseLeft = new(-1, 0);
    public static Transition BaseRight = new(1, 0);
    public static Transition BaseTop = new(0, -1);
    public static Transition BaseBottom = new(0, 1);

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