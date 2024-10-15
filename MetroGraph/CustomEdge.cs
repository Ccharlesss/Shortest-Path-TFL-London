using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroGraph
{
    public class CustomEdge : TaggedEdge<string, double>
    {
        public string Line { get; private set; }

        public CustomEdge(string source, string target, double weight, string line)
            : base(source, target, weight)
        {
            Line = line;
        }
    }
}
