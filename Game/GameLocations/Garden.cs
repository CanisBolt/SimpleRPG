using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLocations
{
    public class Garden
    {
        public Items.GameItems[] Slots { get; set; }
        public int Size { get; set; }

        public Garden()
        {
            Size = 3;
            Slots = new Items.GameItems[Size];
        }
    }
}
