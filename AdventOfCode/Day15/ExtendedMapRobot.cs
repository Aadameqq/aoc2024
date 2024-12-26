namespace Day15;

public class ExtendedMapRobot(ExtendedWarehouse _warehouse)
{
    private readonly ExtendedWarehouse warehouse = _warehouse;
    private Position position = _warehouse.LocateRobot();

    public void ExecuteCommand(Transition transition)
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
            return;
        }

        if (transition.IsHorizontal())
        {
            TryMoveBoxesHorizontally(transition);
            return;
        }

        TryMoveBoxesVertically(transition);
    }

    private void TryMoveBoxesHorizontally(Transition transition)
    {
        var nextPosition = transition.TransitionPosition(position);

        if (!CanMoveBoxesHorizontally(nextPosition, transition)) return;

        MoveBoxesHorizontally(nextPosition, transition);

        warehouse.SetElementAtPosition(nextPosition, Warehouse.Robot);
        warehouse.SetElementAtPosition(position, Warehouse.Empty);

        position = nextPosition;
    }

    private void MoveBoxesHorizontally(Position previousBox, Transition transition)
    {
        var currentBox = transition.TransitionPosition(previousBox);
        if (warehouse.GetElementAtPosition(currentBox) is ExtendedWarehouse.RightBox
            or ExtendedWarehouse.LeftBox)
        {
            MoveBoxesHorizontally(currentBox, transition);
        }

        warehouse.SetElementAtPosition(currentBox, warehouse.GetElementAtPosition(previousBox));
    }

    private bool CanMoveBoxesHorizontally(Position firstBox, Transition transition)
    {
        var possibleNextBox = firstBox;
        while (warehouse.GetElementAtPosition(possibleNextBox) is ExtendedWarehouse.RightBox
               or ExtendedWarehouse.LeftBox)
        {
            possibleNextBox = transition.TransitionPosition(possibleNextBox);
        }

        return warehouse.GetElementAtPosition(possibleNextBox) == Warehouse.Empty;
    }

    private void TryMoveBoxesVertically(Transition transition)
    {
        var nextPosition = transition.TransitionPosition(position);

        if (!CanMoveBoxesVertically(nextPosition, transition)) return;

        MoveBoxesVertically(nextPosition, transition);

        warehouse.SetElementAtPosition(nextPosition, Warehouse.Robot);
        warehouse.SetElementAtPosition(position, Warehouse.Empty);

        position = nextPosition;
    }

    private bool CanMoveBoxesVertically(Position firstPartPosition, Transition transition)
    {
        var isFirstPartOk = CanMoveBoxPart(firstPartPosition, transition);

        var secondPartPosition = GetBoxOtherPartPosition(firstPartPosition);
        var isSecondPartOk = CanMoveBoxPart(secondPartPosition, transition);

        return isFirstPartOk && isSecondPartOk;
    }

    private bool CanMoveBoxPart(Position partPosition, Transition transition)
    {
        var nextPosition = transition.TransitionPosition(partPosition);

        if (warehouse.GetElementAtPosition(nextPosition) == Warehouse.Wall)
        {
            return false;
        }

        if (warehouse.GetElementAtPosition(nextPosition) is ExtendedWarehouse.LeftBox or ExtendedWarehouse.RightBox)
        {
            return CanMoveBoxesVertically(nextPosition, transition);
        }

        return true;
    }

    private void MoveBoxesVertically(Position boxFirstPart, Transition transition)
    {
        var nextBoxFirstPart = transition.TransitionPosition(boxFirstPart);
        var boxSecondPart = GetBoxOtherPartPosition(boxFirstPart);

        var nextBoxSecondPart = transition.TransitionPosition(boxSecondPart);

        if (warehouse.GetElementAtPosition(nextBoxFirstPart) != Warehouse.Empty)
        {
            MoveBoxesVertically(nextBoxFirstPart, transition);
        }

        if (warehouse.GetElementAtPosition(nextBoxSecondPart) != Warehouse.Empty)
        {
            MoveBoxesVertically(nextBoxSecondPart, transition);
        }

        warehouse.SetElementAtPosition(nextBoxFirstPart, warehouse.GetElementAtPosition(boxFirstPart));
        warehouse.SetElementAtPosition(nextBoxSecondPart, warehouse.GetElementAtPosition(boxSecondPart));
        warehouse.SetElementAtPosition(boxFirstPart, Warehouse.Empty);
        warehouse.SetElementAtPosition(boxSecondPart, Warehouse.Empty);
    }

    private Position GetBoxOtherPartPosition(Position boxPart)
    {
        if (warehouse.GetElementAtPosition(boxPart) == ExtendedWarehouse.RightBox)
        {
            return new Transition(-1, 0).TransitionPosition(boxPart);
        }

        return new Transition(1, 0).TransitionPosition(boxPart);
    }
}