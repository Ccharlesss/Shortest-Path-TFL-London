public class Line
{
    public string Name { get; set; }
    public string Direction { get; set; }
    public List<Station> Stations { get; set; }
    public List<Edge> Edges { get; set; }

    public Line(string name, string direction)
    {
        Name = name;
        Direction = direction;
        Stations = new List<Station>();
        Edges = new List<Edge>();
    }

    public void AddStation(Station station)
    {
        if (!Stations.Any(s => s.Name == station.Name))
        {
            Stations.Add(station);
        }
    }

    public void AddEdge(Edge edge)
    {
        if (!Edges.Any(e => e.From == edge.From && e.To == edge.To))
        {
            Edges.Add(edge);
        }
    }
}
