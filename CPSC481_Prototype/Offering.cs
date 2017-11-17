using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC481_Prototype
{
    public class Offering
    {
        public Section Lecture { get; set; }

        public List<Section> Tutorials = new List<Section>();

        public List<Section> Labs = new List<Section>();

    }
}
