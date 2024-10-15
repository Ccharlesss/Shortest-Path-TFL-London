using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroGraph
{
    public class StationConnection
    {
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public double TravelTime { get; set; }
        public string Line { get; set; }
    }

}
