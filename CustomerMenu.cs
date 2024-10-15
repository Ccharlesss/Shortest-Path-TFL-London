namespace MetroMap
{
    public class CustomerMenu
    {
        private StationGraph stationGraph;

        public CustomerMenu(StationGraph stationGraph)
        {
            this.stationGraph = stationGraph;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Metro Map Customer Menu");
            Console.WriteLine("1. Find Shortest Path");
            Console.WriteLine("2. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 2.");
                Console.Write("Enter your choice: ");
            }

            switch (choice)
            {
                case 1:
                    FindShortestPath();
                    break;
                case 2:
                    Console.WriteLine("Exiting...");
                    break;
            }
        }

        public void FindShortestPath()
        {
            Station startStation = null;
            while (startStation == null)
            {
                Console.Write("Enter the start station name: ");
                string startName = Console.ReadLine();
                startStation = stationGraph.GetStation(startName);
                if (startStation == null)
                {
                    Console.WriteLine("Start station not found. Please try again.");
                }
            }

            Station endStation = null;
            while (endStation == null)
            {
                Console.Write("Enter the end station name: ");
                string endName = Console.ReadLine();
                endStation = stationGraph.GetStation(endName);
                if (endStation == null)
                {
                    Console.WriteLine("End station not found. Please try again.");
                }
            }

            var result = stationGraph.FindShortestPath(startStation, endStation);
            var path = result.path;
            double totalTime = result.time;

            if (path.Count == 0)
            {
                Console.WriteLine("No valid route found.");
                return;
            }

            Console.WriteLine($"Route: {startStation.Name} to {endStation.Name}:");
            int stepNumber = 1;
            Line previousLine = null;

            for (int i = 0; i < path.Count; i++)
            {
                var (currentStation, currentLine) = path[i];
                if (currentLine == null)
                {
                    currentLine = path[i + 1].Item2;
                };

                if (i == 0)
                {
                    Console.WriteLine($"({stepNumber++}) Start: {currentStation.Name}, {currentLine.Name} ({currentLine.Direction})");
                    previousLine = currentLine;
                }
                else
                {
                    var (previousStation, _) = path[i - 1];
                    var edge = stationGraph.GetEdge(previousStation, currentStation);

                    if (currentLine != previousLine)
                    {
                        Console.WriteLine($"({stepNumber++}) Change: {previousStation.Name} {previousLine.Name} ({previousLine.Direction}) to {currentLine.Name} ({currentLine.Direction}) 2.00min");
                    }

                    Console.WriteLine($"({stepNumber++}) {currentLine.Name} ({currentLine.Direction}): {previousStation.Name} to {currentStation.Name} {edge.Time:F2}min");

                    previousLine = currentLine;
                }
            }

            Console.WriteLine($"({stepNumber}) End: {endStation.Name}, {previousLine.Name} ({previousLine.Direction})");
            Console.WriteLine($"Total Journey Time: {totalTime:F2} minutes");
        }
    }
}
