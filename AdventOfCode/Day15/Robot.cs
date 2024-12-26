namespace Day15;

public class Robot(Warehouse _warehouse)
{
    private readonly Warehouse warehouse = _warehouse;
    private Position position = _warehouse.LocateRobot();


    public void Move(Transition transition)
    {
        var nextPosition = transition.TransitionPosition(position);

        if (warehouse.GetElementAtPosition(nextPosition) == Warehouse.Wall)
        {
            return;
        }

        if (warehouse.GetElementAtPosition(nextPosition) == Warehouse.Empty)
        {
            warehouse.SetElementAtPosition(position, Warehouse.Empty);
            warehouse.SetElementAtPosition(nextPosition, Warehouse.Robot);
            position = nextPosition;
        }

        TryMoveBoxes(nextPosition, transition);
    }

    private void TryMoveBoxes(Position firstBox, Transition transition)
    {
        var nextBox = firstBox;

        while (warehouse.GetElementAtPosition(nextBox) == Warehouse.Box)
        {
            nextBox = transition.TransitionPosition(nextBox);
        }

        if (warehouse.GetElementAtPosition(nextBox) == Warehouse.Wall) return;

        warehouse.SetElementAtPosition(nextBox, Warehouse.Box);
        warehouse.SetElementAtPosition(position, Warehouse.Empty);
        warehouse.SetElementAtPosition(firstBox, Warehouse.Robot);
        position = firstBox;
    }
}