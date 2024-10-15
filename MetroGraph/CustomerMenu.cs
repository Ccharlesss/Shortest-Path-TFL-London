using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MetroGraph
{
    public class CustomerMenu
    {
        private GraphManager _stationGraph;

        public CustomerMenu(GraphManager stationGraph)
        {
            _stationGraph = stationGraph;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("--------------------------------");
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
                    Console.WriteLine();
                    PromptForShortestPath();
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("Exiting...");
                    Console.WriteLine();
                    break;
            }
        }

        private void PromptForShortestPath()
        {
            
            Console.Write("Enter start station: ");
            string startStation = Console.ReadLine().Trim();

            Console.Write("Enter destination station: ");
            string destinationStation = Console.ReadLine().Trim();

            _stationGraph.FindShortestPath(startStation, destinationStation);

        }

    }
}
