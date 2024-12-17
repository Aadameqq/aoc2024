namespace Day12;

public class Plot
{
    public int FieldsAmount { get; private set; } = 1;
    public int Perimeter { get; private set; } = 4;

    public void MergeWith(Plot second)
    {
        FieldsAmount += second.FieldsAmount;
        Perimeter += second.Perimeter;
        // Perimeter--;
    }

    public int CalculateCost()
    {
        return FieldsAmount * Perimeter;
    }

    public void DecreasePerimeter()
    {
        Perimeter--;
    }
}