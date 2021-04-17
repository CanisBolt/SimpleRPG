using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLocations
{
    public class Location
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public Region Region { get; set; }
    }
}
