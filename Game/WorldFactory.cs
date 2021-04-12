using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class WorldFactory
    {
        internal World CreateWorld()
        {
            World newWorld = new World();

            newWorld.CreateRaces("Human", 0, 0, 0, 0, 0, 0, 0);
            newWorld.CreateRaces("Elf", -2, 2, -2, 1, 0, 1, 1);
            newWorld.CreateRaces("Dwarf", 2, -2, 2, -2, 0, 0, 2);
            newWorld.CreateRaces("DogFolk", -1, -2, 0, 2, 2, -1, 3);
            newWorld.CreateRaces("CatFolk", -2, 2, -1, 0, -1, 2, 4);

            newWorld.AddLocation(0, 0, "Home",
                "Home sweet home");

            newWorld.AddLocation(0, 1, "House garden",
                "This is your garden");

            return newWorld;
        }
    }
}