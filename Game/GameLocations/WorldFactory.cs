using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLocations
{
    internal class WorldFactory
    {
        internal World CreateWorld()
        {
            World newWorld = new World();

            newWorld.AddRegion("Village", World.RegionIDVillage);
            newWorld.RegionByID(World.RegionIDVillage).AddEnemy(EnemyFactory.GetMonster(World.EnemyIDSnake));
            newWorld.RegionByID(World.RegionIDVillage).AddEnemy(EnemyFactory.GetMonster(World.EnemyIDRat));

            newWorld.CreateRaces("Human", 0, 0, 0, 0, 0, 0, World.RaceIDHuman);
            newWorld.CreateRaces("Elf", -2, 2, -2, 1, 0, 1, World.RaceIDElf);
            newWorld.CreateRaces("Dwarf", 2, -2, 2, -2, 0, 0, World.RaceIDDwarf);
            newWorld.CreateRaces("DogFolk", -1, -2, 0, 2, 2, -1, World.RaceIDDogFolk);
            newWorld.CreateRaces("CatFolk", -2, 2, -1, 0, -1, 2, World.RaceIDCatFolk);

            newWorld.AddLocation(0, 0, "Home",
                "Home sweet home", newWorld.RegionByID(World.RegionIDVillage));

            newWorld.AddLocation(0, 1, "House garden",
                "This is your garden", newWorld.RegionByID(World.RegionIDVillage));

            return newWorld;
        }
    }
}