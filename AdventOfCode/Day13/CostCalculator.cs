namespace Day13;

public class CostCalculator(bool shouldRestrictPresses = true)
{
    private const int AvailablePressesPerButton = 100;
    private const int BPressCost = 1;
    private const int APressCost = 3;

    public long CalculateCost(Position buttonA, Position buttonB, Position target)
    {
        var mainDet = CalculateDeterminant(buttonA.X, buttonA.Y, buttonB.X, buttonB.Y);
        var xDet = CalculateDeterminant(target.X, target.Y, buttonB.X, buttonB.Y);
        var yDet = CalculateDeterminant(buttonA.X, buttonA.Y, target.X, target.Y);

        if (!HasSingleSolution(mainDet, xDet, yDet)) return 0;

        var aPresses = xDet / mainDet;
        var bPresses = yDet / mainDet;

        if (shouldRestrictPresses &&
            (aPresses > AvailablePressesPerButton || bPresses > AvailablePressesPerButton)) return 0;

        return aPresses * APressCost + bPresses * BPressCost;
    }

    private static long CalculateDeterminant(long x, long y, long a, long b)
    {
        return x * b - a * y;
    }

    private static bool HasSingleSolution(long mainDet, long xDet, long yDet)
    {
        return mainDet != 0 && xDet % mainDet == 0 && yDet % mainDet == 0;
    }
}