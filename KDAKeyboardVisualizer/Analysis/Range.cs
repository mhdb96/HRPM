using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDAKeyboardVisualizer.Analysis
{
    public class Range
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public List<Tuple<int, int>> Elements { get; set; } = new List<Tuple<int, int>>();

        public void FindMinMax()
        {
            if (Elements.Any())
            {
                Max = Elements.Max(x => x.Item2);
                Min = Elements.Min(x => x.Item2);
            }
            
        }

    }
}
