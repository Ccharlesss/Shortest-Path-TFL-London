using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroGraph
{
    public class EngineerMenu
    {
        private GraphManager _stationGraph;

        public EngineerMenu(GraphManager stationGraph)
        {
            _stationGraph = stationGraph;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Metro Map Engineer Menu");
            Console.WriteLine("1. View Traffic Info");
            Console.WriteLine("2. View Stations");
            Console.WriteLine("3. Add Delay Time");
            Console.WriteLine("4. Remove Delay Time");
            Console.WriteLine("5. Change Track Status (open/close)");
            Console.WriteLine("6. View Closed Tracks");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                Console.Write("Enter your choice: ");
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine();
                    ViewTrafficInfo();
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine();
                    ViewStations();
                    Console.WriteLine();
                    break;
                case 3:
                    Console.WriteLine();
                    AddDelay();
                    Console.WriteLine();
                    break;
                case 4:
                    Console.WriteLine();
                    RemoveDelay();
                    Console.WriteLine();
                    break;
                case 5:
                    Console.WriteLine();
                    ChangeTrackStatus();
                    Console.WriteLine();
                    break;
                case 6:
                    Console.WriteLine();
                    _stationGraph.ListClosedTracks();
                    Console.WriteLine();
                    break;
                case 7:
                    Console.WriteLine("Exiting...");
                    Console.WriteLine();
                    break;
            }
        }

        private void ViewTrafficInfo()
        {
            Console.WriteLine("Traffic information:");
            _stationGraph.DisplayTrafficInfo();
        }

        private void ViewStations()
        {
            // List all stations
            Console.WriteLine("Listing all stations:");
            _stationGraph.DisplayStations();
        }

        private void AddDelay()
        {
            _stationGraph.ListActiveDelays();

            Console.Write("Enter start station: ");
            string start = Console.ReadLine();
            Console.Write("Enter end station: ");
            string end = Console.ReadLine();
            Console.Write("Enter delay time (in minutes): ");
            int delay = int.Parse(Console.ReadLine());

            _stationGraph.SetDelayBetweenStations(start, end, delay);
            Console.WriteLine($"Added delay of {delay} minutes between {start} and {end}.");
        }

        private void RemoveDelay()
        {
            _stationGraph.ListActiveDelays();

            Console.Write("Enter start station: ");
            string start = Console.ReadLine();
            Console.Write("Enter end station: ");
            string end = Console.ReadLine();

            _stationGraph.RemoveDelay(start, end);
            Console.WriteLine($"Removed delay between {start} and {end}.");
        }

        private void ChangeTrackStatus()
        {
            Console.Write("Enter start station: ");
            string start = Console.ReadLine();
            Console.Write("Enter end station: ");
            string end = Console.ReadLine();
            Console.Write("Enter status (open/close): ");
            string status = Console.ReadLine().Trim().ToLower();

            _stationGraph.ChangeTrackStatus(start, end, status);
            Console.WriteLine($"Track status between {start} and {end} has been updated to {status}.");
        }
    }

}
