using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetroMap
{
    public class EngineerMenu
    {
        private StationGraph stationGraph;

        public EngineerMenu(StationGraph stationGraph)
        {
            this.stationGraph = stationGraph;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Metro Map Engineer Menu");
            Console.WriteLine("1. View Traffic Info");
            Console.WriteLine("2. View Stations");
            Console.WriteLine("3. Add Delay Time");
            Console.WriteLine("4. Remove Delay Time");
            Console.WriteLine("5. Change track Status (open/close)");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                Console.Write("Enter your choice: ");
            }

            switch (choice)
            {
                case 1:
                    ViewTrafficInfo();
                    break;
                case 2:
                    ViewStations();
                    break;
                case 3:
                    AddDelay();
                    break;
                case 4:
                    RemoveDelay();
                    break;
                case 5:
                    ChangeTrackStatus();
                    break;
                case 6:
                    Console.WriteLine("Exiting...");
                    break;
            }
        }

        public void ViewTrafficInfo()
        {
            Console.WriteLine("List of closed track sections:");
            foreach (Edge edge in stationGraph.Edges)
            {
                if (edge.IsClosed)
                {
                    Console.WriteLine($"  From {edge.From.Name} To {edge.To.Name}");
                }
            }
        }

        public void ViewStations()
        {
            foreach (var line in stationGraph.Lines)
            {
                Console.WriteLine($"Line: {line.Name} ({line.Direction})");

                foreach (var station in line.Stations)
                {
                    Console.WriteLine($"  {station.Name}");
                }
            }
        }

        public void AddDelay()
        {
            Console.WriteLine("Enter the name of the first station:");
            string firstStationName = Console.ReadLine();
            Console.WriteLine("Enter the name of the second station:");
            string secondStationName = Console.ReadLine();
            Console.WriteLine("Enter the delay time (in minutes):");
            double delayTime;

            // Validates the delay input as a double
            while (!double.TryParse(Console.ReadLine(), out delayTime) || delayTime < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number for delay time:");
            }

            // Find the edge
            Edge edge = stationGraph.Edges.FirstOrDefault(e => e.From.Name.Equals(firstStationName, StringComparison.OrdinalIgnoreCase)
                                               && e.To.Name.Equals(secondStationName, StringComparison.OrdinalIgnoreCase));

            if (edge != null)
            {
                // Add delay
                edge.Delay += delayTime;
                Console.WriteLine($"Added {delayTime} minutes of delay between {firstStationName} and {secondStationName}.");
            }
            else
            {
                Console.WriteLine("No direct edge found between the specified stations.");
            }
        }

        public void RemoveDelay()
        {
            Console.WriteLine("Enter the name of the first station:");
            string firstStationName = Console.ReadLine();
            Console.WriteLine("Enter the name of the second station:");
            string secondStationName = Console.ReadLine();


            // Find the edge
            Edge edge = stationGraph.Edges.FirstOrDefault(e => e.From.Name.Equals(firstStationName, StringComparison.OrdinalIgnoreCase)
                                               && e.To.Name.Equals(secondStationName, StringComparison.OrdinalIgnoreCase));

            if (edge != null)
            {
                // Add delay
                edge.Delay = 0;
                Console.WriteLine($"Removed delay between {firstStationName} and {secondStationName}.");
            }
            else
            {
                Console.WriteLine("No direct edge found between the specified stations.");
            }
        }

        public void ChangeTrackStatus()
        {
            Console.WriteLine("Enter the name of the first station:");
            string firstStationName = Console.ReadLine();
            Console.WriteLine("Enter the name of the second station:");
            string secondStationName = Console.ReadLine();


            // Find the edge
            Edge edge = stationGraph.Edges.FirstOrDefault(e => e.From.Name.Equals(firstStationName, StringComparison.OrdinalIgnoreCase)
                                               && e.To.Name.Equals(secondStationName, StringComparison.OrdinalIgnoreCase));

            if (edge != null)
            {
                if (edge.IsClosed)
                {
                    edge.IsClosed = false;
                }
                else
                {
                    edge.IsClosed = true;
                }
                Console.WriteLine($"Track status changed between {firstStationName} and {secondStationName}.");
            }
            else
            {
                Console.WriteLine("No direct edge found between the specified stations.");
            }
        }
    }
}