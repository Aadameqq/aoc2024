namespace Day6;

public record Position(int X, int Y)
{
    public Position ApplyTransition(Transition transition)
    {
        return new Position(X + transition.X, Y + transition.Y);
    }
}