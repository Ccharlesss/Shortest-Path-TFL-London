public class Edge
{
    public Station From { get; set; }
    public Station To { get; set; }
    public Line Line { get; set; }
    public double Time { get; set; }
    public double Distance { get; set; }
    public double Delay { get; set; } = 0;
    public bool IsClosed { get; set; } = false;

    public Edge(Station from, Station to, double time, double distance)
    {
        From = from;
        To = to;
        Time = time;
        Distance = distance;
    }
}
