using Day15;

const string dataFile = "../../../data/data.txt";

List<char[]> map = [];
List<char[]> mapForExtended = [];
List<char> commands = [];

foreach (var line in File.ReadLines(dataFile))
{
    if (line.StartsWith(Warehouse.Wall))
    {
        map.Add(line.ToCharArray());
        mapForExtended.Add(line.ToCharArray());
        continue;
    }

    if (line.Length == 0) continue;

    commands.AddRange(line.ToCharArray());
}

Dictionary<char, Transition> directions = new()
{
    { '^', Transition.BaseTop },
    { '>', Transition.BaseRight },
    { '<', Transition.BaseLeft },
    { 'v', Transition.BaseBottom }
};

var warehouse = new Warehouse(map);
var robot = new Robot(warehouse);
var extendedWarehouse = new ExtendedWarehouse(mapForExtended);
var extendedWarehouseRobot = new ExtendedWarehouseRobot(extendedWarehouse);

foreach (var command in commands)
{
    var transition = directions[command];
    robot.Move(transition);
    extendedWarehouseRobot.Move(transition);
}

var sum = warehouse.CalculateCoordinatesSum();
var extendedWarehouseSum = extendedWarehouse.CalculateCoordinatesSum();

Console.WriteLine($"Sum of coordinates: {sum}");
Console.WriteLine($"Sum of coordinates - extended warehouse: {extendedWarehouseSum}");