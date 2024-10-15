using MetroMap;

string filePath = "./Data/data.csv"; // The path to the CSV file.
// string filePath = "E:/Westminster/Metro/MetroMap/Data/data.csv"; // The path to the CSV file.

StationGraph map = new StationGraph();

if (!File.Exists(filePath))
{
    Console.WriteLine("File not found: " + filePath);
    return;
}

var lines = File.ReadAllLines(filePath);
Dictionary<string, Line> lineDictionary = new Dictionary<string, Line>();

for (int i = 1; i < lines.Length; i++)
{
    var fields = lines[i].Split(',');
    string lineName = fields[0].Trim();
    string direction = fields[1].Trim();
    string stationFromName = fields[2].Trim();
    string stationToName = fields[3].Trim();
    double distance = double.Parse(fields[4].Trim());
    double time = double.Parse(fields[5].Trim());

    if (!lineDictionary.ContainsKey($"{lineName}({direction})"))
    {
        lineDictionary[$"{lineName}({direction})"] = new Line(lineName, direction);
        map.AddLine(lineDictionary[$"{lineName}({direction})"]);
    }

    Station stationFrom = map.GetStation(stationFromName) ?? new Station(stationFromName);
    Station stationTo = map.GetStation(stationToName) ?? new Station(stationToName);

    map.AddStation(stationFrom);
    map.AddStation(stationTo);

    var edge = new Edge(stationFrom, stationTo, time, distance);
    lineDictionary[$"{lineName}({direction})"].AddEdge(edge);
    edge.Line = lineDictionary[$"{lineName}({direction})"];
    stationFrom.AddEdge(edge);
    stationFrom.AddLine(lineDictionary[$"{lineName}({direction})"]);
    stationTo.AddEdge(edge);
    stationTo.AddLine(lineDictionary[$"{lineName}({direction})"]);
    map.AddEdge(edge);

    lineDictionary[$"{lineName}({direction})"].AddStation(stationFrom);
    lineDictionary[$"{lineName}({direction})"].AddStation(stationTo);
}

bool isRunning = true; // Flag to control the main loop
Console.WriteLine("Welcome to Metro Map System!");

while (isRunning)
{
    Console.WriteLine("Select user type:");
    Console.WriteLine("1. Engineer");
    Console.WriteLine("2. Customer");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");

    int userType;
    while (!int.TryParse(Console.ReadLine(), out userType) || (userType < 0 || userType > 2))
    {
        Console.WriteLine("Invalid choice. Please enter 1 for Engineer, 2 for Customer, or 0 to Exit.");
        Console.Write("Enter your choice: ");
    }

    switch (userType)
    {
        case 1:
            EngineerMenu engineerMenu = new EngineerMenu(map);
            engineerMenu.DisplayMenu();
            break;
        case 2:
            CustomerMenu customerMenu = new CustomerMenu(map);
            customerMenu.DisplayMenu();
            break;
        case 0:
            Console.WriteLine("Exiting the Metro Map System.");
            isRunning = false;
            break;
    }
}

Console.WriteLine("Thank you for using Metro Map System!");
