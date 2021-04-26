using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.SpecialAttack;

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
            newWorld.RegionByID(World.RegionIDVillage).AddEnemy(EnemyFactory.GetMonster(World.EnemyIDGoblin));

            newWorld.AddRace("Human", 0, 0, 0, 0, 0, 0, World.RaceIDHuman);
            newWorld.AddRace("Elf", -2, 2, -2, 1, 0, 1, World.RaceIDElf);
            newWorld.AddRace("Dwarf", 2, -2, 2, -2, 0, 0, World.RaceIDDwarf);
            newWorld.AddRace("DogFolk", -1, -2, 0, 2, 2, -1, World.RaceIDDogFolk);
            newWorld.AddRace("CatFolk", -2, 2, -1, 0, -1, 2, World.RaceIDCatFolk);

            newWorld.AddMagic("Fireball", World.MagicIDFireball, "Throw a fireball to enemy. Base Damage: 3 + 0.8 for each Intelligence point. Mana cost: 5", 3f, 5, 0.8f, SkillsAndMagic.Target.Enemy);
            newWorld.AddMagic("IceArrow", World.MagicIDIceArrow, "Create an arrow of ice and shoot it to the enemy. Base Damage: 2 + 1.2 for each Intelligence point. Mana cost: 7", 1f, 7, 1.2f, SkillsAndMagic.Target.Enemy);
            newWorld.AddMagic("ThunderStrike", World.MagicIDThunderStrike, "Summon a thunder than strike the enemy. Base Damage: 5 + 1 for each Intelligence point. Mana cost: 10", 5f, 10, 1f, SkillsAndMagic.Target.Enemy);
            newWorld.AddMagic("Small Heal", World.MagicIDSmallHeal, "Cure yourself with magic. Base healing: 5 + 1.5 for Intelligence point. Mana cost: 10", 5, 10, 1.5f, SkillsAndMagic.Target.Self);


            newWorld.AddSkill("Sword: Fast Strike", World.SwordSKillIDFastStrike, "Deal fast strike with sword. Base Damage: 3 + 0.8 for each Strength point. Mana Cost: 5", 3, 5, 0.8f, 1, SkillsAndMagic.Target.Enemy);
            newWorld.AddSkill("Sword: Heavy Strike", World.SwordSKillIDHeavyStrike, "Deal heavy strike with sword. Base Damage: 5 + 1.5 for each Strength point. Mana Cost 15", 5, 15, 1.5f, 1, SkillsAndMagic.Target.Enemy);
            newWorld.AddSkill("Sword: MultiHit", World.SwordSKillIDMultiHit, "Deals multiple hits (from 2 to 5). Base Damage from 1 hit: 2 + 0.5 for each Strength point. Mana Cost 10", 2, 10, 0.5f, 5, SkillsAndMagic.Target.Enemy);

            newWorld.AddLocation(0, 0, "Home",
                "Home sweet home", newWorld.RegionByID(World.RegionIDVillage));

            newWorld.AddLocation(0, 1, "House garden",
                "This is your garden", newWorld.RegionByID(World.RegionIDVillage));

            return newWorld;
        }
    }
}