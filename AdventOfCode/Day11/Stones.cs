using System.Collections;

namespace Day11;

public class Stones : IEnumerable<string>
{
    private readonly Dictionary<string, long> dict = new();

    public Stones(IEnumerable<string> stonesEnumerable)
    {
        foreach (var stone in stonesEnumerable)
            if (!dict.TryAdd(stone, 1))
                dict[stone]++;
    }

    public IEnumerator<string> GetEnumerator()
    {
        return dict.Keys.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public StonesChanges DumpForChanges()
    {
        return new StonesChanges(dict);
    }

    public void ApplyChanges(StonesChanges changes)
    {
        foreach (var stone in changes.Dict.Keys) dict[stone] = changes.Dict[stone];
    }

    public long GetStoneCount(string key)
    {
        return dict[key];
    }

    public long Count()
    {
        return dict.Keys.Sum(stone => dict[stone]);
    }
}