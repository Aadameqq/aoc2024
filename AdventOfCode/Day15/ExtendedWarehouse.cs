namespace Day15;

public class ExtendedWarehouse : Warehouse
{
    public const char LeftBox = '[';
    public const char RightBox = ']';

    public ExtendedWarehouse(List<char[]> _map)
    {
        map = [];

        foreach (var row in _map)
        {
            List<char> colElements = [];

            foreach (var col in row)
            {
                if (col == Box)
                {
                    colElements.AddRange([LeftBox, RightBox]);
                    continue;
                }

                if (col == Robot)
                {
                    colElements.AddRange([Robot, Empty]);
                    continue;
                }

                colElements.AddRange([col, col]);
            }

            map.Add(colElements.ToArray());
        }
    }

    public long CalculateCoordinatesSum()
    {
        return CalculateCoordinatesSum(LeftBox);
    }
}