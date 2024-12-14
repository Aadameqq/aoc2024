namespace Day11;

public class StonesChanges(Dictionary<string, long> _dict)
{
    public Dictionary<string, long> Dict { get; } = new(_dict);

    public void OverrideStoneCount(string key, long value)
    {
        if (!Dict.TryAdd(key, value)) Dict[key] = value;
    }

    public void IncreaseStoneCount(string key, long value)
    {
        OverrideStoneCount(key, value + GetValueOrDefault(key));
    }

    public void DecreaseStoneCount(string key, long value)
    {
        OverrideStoneCount(key, GetValueOrDefault(key) - value);
    }

    private long GetValueOrDefault(string key)
    {
        return Dict.GetValueOrDefault(key, 0);
    }
}