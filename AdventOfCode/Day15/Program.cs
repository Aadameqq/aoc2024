using Day15;

const string dataFile = "../../../data/data.txt";

List<char[]> map = [];
List<char> commands = [];

foreach (var line in File.ReadLines(dataFile))
{
    if (line.StartsWith(Warehouse.Wall))
    {
        map.Add(line.ToCharArray());
        continue;
    }

    if (line.Length == 0) continue;

    commands.AddRange(line.ToCharArray());
}

var warehouse = new Warehouse(map);
var extendedWarehouse = new ExtendedWarehouse(map);
var robot = new Robot(warehouse);
var extendedWarehouseRobot = new ExtendedMapRobot(extendedWarehouse);


Dictionary<char, Transition> directions = new()
{
    { '^', new Transition(0, -1) },
    { '>', new Transition(1, 0) },
    { '<', new Transition(-1, 0) },
    { 'v', new Transition(0, 1) }
};

foreach (var command in commands)
{
    var transition = directions[command];
    robot.ExecuteCommand(transition);
}

var sum = warehouse.CalculateCoordinatesSum();

Console.WriteLine($"Sum of coordinates: {sum}");

foreach (var command in commands)
{
    var transition = directions[command];
    extendedWarehouseRobot.ExecuteCommand(transition);
}

var extendedWarehouseSum = extendedWarehouse.CalculateCoordinatesSum();

Console.WriteLine($"Sum of coordinates - extended warehouse: {extendedWarehouseSum}");