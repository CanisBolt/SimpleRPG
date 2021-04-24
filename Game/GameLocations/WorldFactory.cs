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

            newWorld.AddRace("Human", 0, 0, 0, 0, 0, 0, World.RaceIDHuman);
            newWorld.AddRace("Elf", -2, 2, -2, 1, 0, 1, World.RaceIDElf);
            newWorld.AddRace("Dwarf", 2, -2, 2, -2, 0, 0, World.RaceIDDwarf);
            newWorld.AddRace("DogFolk", -1, -2, 0, 2, 2, -1, World.RaceIDDogFolk);
            newWorld.AddRace("CatFolk", -2, 2, -1, 0, -1, 2, World.RaceIDCatFolk);

            newWorld.AddMagic("Fireball", World.MagicIDFireball, "Throw a fireball to enemy. Base damage: 3 + 0.8 for each Intelligence point. Mana cost: 5", 3f, 5, 0.8f);
            newWorld.AddMagic("IceArrow", World.MagicIDIceArrow, "Create an arrow of ice and shoot it to the enemy. Base damage: 2 + 1.2 for each Intelligence point. Mana cost: 7", 1f, 7, 1.2f);
            newWorld.AddMagic("ThunderStrike", World.MagicIDThunderStrike, "Summon a thunder than strike the enemy. Base damage: 5 + 1 for each Intelligence point. Mana cost: 10", 5f, 10, 1f);

            newWorld.AddLocation(0, 0, "Home",
                "Home sweet home", newWorld.RegionByID(World.RegionIDVillage));

            newWorld.AddLocation(0, 1, "House garden",
                "This is your garden", newWorld.RegionByID(World.RegionIDVillage));

            return newWorld;
        }
    }
}