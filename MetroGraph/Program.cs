using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroGraph
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var reader = new CsvReader();

            var baseDirectory = @"E:\Westminster\Metro\MetroMap\MetroGraph";
            //var baseDirectory = @"H:\Version 2 using quick graph\MetroGraph\MetroGraph";
            var filePath = Path.Combine(baseDirectory, "Data", "data.csv");
            var connections = reader.ReadStationConnections(filePath);

            var manager = new GraphManager();
            manager.BuildGraph(connections);

            EngineerMenu engineerMenu = new EngineerMenu(manager);
            CustomerMenu customerMenu = new CustomerMenu(manager);


            while (true)
            {
                Console.WriteLine("Welcome to Metro Map System");
                Console.WriteLine("1. Customer Menu");
                Console.WriteLine("2. Engineer Menu");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        customerMenu.DisplayMenu();
                        break;
                    case "2":
                        engineerMenu.DisplayMenu();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

    }

}
