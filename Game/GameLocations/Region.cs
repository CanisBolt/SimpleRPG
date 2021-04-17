using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLocations
{
    public class Region
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public bool IsSafeRegion { get; set; }

        // Collect locations in region and attach monsters to region, not location itself
    }
}
