using QuickGraph;
using QuickGraph.Algorithms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroGraph;

namespace MetroGraph
{
    public class GraphManager
    {
        private AdjacencyGraph<string, CustomEdge> graph = new AdjacencyGraph<string, CustomEdge>();
        Dictionary<(string, string), CustomEdge> closedTracks = new Dictionary<(string, string), CustomEdge>();
        private Dictionary<(string, string), (CustomEdge edge, double originalTime, double delayTime)> activeDelays = new Dictionary<(string, string), (CustomEdge, double, double)>();


        List<StationConnection> stationConnections = new List<StationConnection>();

        public void BuildGraph(List<StationConnection> connections)
        {
            stationConnections = connections;
            foreach (var connection in connections)
            {
                graph.AddVertex(connection.StartStation);
                graph.AddVertex(connection.EndStation);
                var edge = new CustomEdge(connection.StartStation, connection.EndStation, connection.TravelTime, connection.Line);
                graph.AddEdge(edge);
            }
        }



        public void FindShortestPath(string startStation, string endStation)
        {
            Func<CustomEdge, double> edgeCost = e => e.Tag;  
            var tryGetPath = graph.ShortestPathsDijkstra(edgeCost, startStation);
            string previousLine = null;  
            double totalTravelTime = 0;  

            if (tryGetPath(endStation, out IEnumerable<CustomEdge> path))
            {
                Console.WriteLine("_________Journey Plan_________");
                foreach (var edge in path)
                {
                    double edgeTravelTime = edge.Tag;  
                    if (previousLine != null && previousLine != edge.Line)
                    {
                        double lineChangePenalty = 2; 
                        totalTravelTime += lineChangePenalty; 
                        Console.WriteLine($"Line changing from {previousLine} to {edge.Line} will take {lineChangePenalty} min.");
                    }
                    previousLine = edge.Line; 

                    totalTravelTime += edgeTravelTime; 
                    Console.WriteLine($"{edge.Source} to {edge.Target} on {edge.Line} takes {edgeTravelTime} minutes");
                }
                Console.WriteLine($"Total travel time: {totalTravelTime} minutes.");
            }
            else
            {
                Console.WriteLine("No path found.");
            }
        }


        private StationConnection FindConnection(string startStation, string endStation)
        {
            return stationConnections.FirstOrDefault(c => c.StartStation == startStation && c.EndStation == endStation);
        }

        public void SetDelayBetweenStations(string startStation, string endStation, double additionalTime)
        {
            ListActiveDelays();

            var edge = graph.Edges.FirstOrDefault(e => e.Source == startStation && e.Target == endStation);
            if (edge != null)
            {
                var updatedEdge = new CustomEdge(edge.Source, edge.Target, edge.Tag + additionalTime, edge.Line);
                graph.RemoveEdge(edge);
                graph.AddEdge(updatedEdge);

                activeDelays[(startStation, endStation)] = (updatedEdge, edge.Tag, additionalTime);
                Console.WriteLine($"Added a delay of {additionalTime} minutes between {startStation} and {endStation}.");
            }
            else
            {
                Console.WriteLine("Track not found between the specified stations.");
            }
        }



        public void ListActiveDelays()
        {
            if (activeDelays.Any())
            {
                Console.WriteLine("Active delays between stations:");
                foreach (var delay in activeDelays)
                {
                    Console.WriteLine($"{delay.Key.Item1} to {delay.Key.Item2}: Delayed time = {delay.Value.delayTime} minutes (Original time = {delay.Value.originalTime} minutes)");
                }
            }
            else
            {
                Console.WriteLine("There are no active delays.");
            }
        }


        public void ListClosedTracks()
        {
            if (closedTracks.Any())
            {
                Console.WriteLine("Currently closed tracks:");
                foreach (var track in closedTracks.Keys)
                {
                    Console.WriteLine($"{track.Item1} to {track.Item2}");
                }
            }
            else
            {
                Console.WriteLine("No tracks are currently closed.");
            }
        }

        public void DisplayTrafficInfo()
        {
            int totalEdges = graph.EdgeCount;
            int closedEdges = closedTracks.Count;
            int openEdges = totalEdges - closedEdges;

            Console.WriteLine($"Total tracks: {totalEdges}");
            Console.WriteLine($"Open tracks: {openEdges}");
            Console.WriteLine($"Closed tracks: {closedEdges}");
            Console.WriteLine();
            Console.WriteLine("Delay informations");
            ListActiveDelays();
        }

        public void DisplayStations()
        {
            var stations = new HashSet<string>(graph.Vertices);
            Console.WriteLine("List of all stations:");
            foreach (var station in stations)
            {
                Console.WriteLine(station);
            }
        }




        public void RemoveDelay(string startStation, string endStation)
        {
            ListActiveDelays();

            var key = (startStation, endStation);
            if (activeDelays.TryGetValue(key, out var delayInfo))
            {
                graph.RemoveEdge(delayInfo.edge);

                var restoredEdge = new CustomEdge(startStation, endStation, delayInfo.originalTime, delayInfo.edge.Line);
                graph.AddEdge(restoredEdge);

                activeDelays.Remove(key);
                Console.WriteLine($"Delay removed. Restored original travel time between {startStation} and {endStation}.");
            }
            else
            {
                Console.WriteLine("No delay to remove between the specified stations.");
            }
        }




        public void ChangeTrackStatus(string startStation, string endStation, string status)
        {
            var key = (startStation, endStation);
            if (status.ToLower() == "close")
            {
                var edge = graph.Edges.FirstOrDefault(e => e.Source == startStation && e.Target == endStation);
                if (edge != null)
                {
                    graph.RemoveEdge(edge);
                    closedTracks[key] = edge;
                    Console.WriteLine($"Track closed between {startStation} and {endStation}.");
                }
                else
                {
                    Console.WriteLine("No track found between specified stations or track already closed.");
                }
            }
            else if (status.ToLower() == "open")
            {
                if (closedTracks.TryGetValue(key, out var edge))
                {
                    graph.AddEdge(edge);
                    closedTracks.Remove(key);
                    Console.WriteLine($"Track reopened between {startStation} and {endStation}.");
                }
                else
                {
                    Console.WriteLine("Track is not closed or does not exist.");
                }
            }
        }



    }
}
