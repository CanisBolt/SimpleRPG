using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.GameLocations;
using Game.LivingCreatures;
using Game.SpecialAttack;

namespace Game
{
    public class World
    {
        private static List<Race> allRaces = new List<Race>();
        private List<Location> allLocations = new List<Location>();
        private List<Region> allRegions = new List<Region>();
        private static List<Magic> allMagicSpells = new List<Magic>();
        private static List<WeaponSkills> allSkills = new List<WeaponSkills>();

        // ID's

        // Races
        public static int RaceIDHuman = 0;
        public static int RaceIDElf = 1;
        public static int RaceIDDwarf = 2;
        public static int RaceIDDogFolk = 3;
        public static int RaceIDCatFolk = 4;

        // Regions
        public static int RegionIDVillage = 0;
        public static int RegionIDForest = 1;

        // Enemies
        public static int EnemyIDSnake = 0;
        public static int EnemyIDRat = 1;
        public static int EnemyIDGoblin = 2;
        public static int EnemyIDWolf = 3;
        public static int EnemyIDRogue = 4;

        // Magic
        public static int MagicIDFireball = 0;
        public static int MagicIDIceArrow = 1;
        public static int MagicIDThunderStrike = 2;
        public static int MagicIDSmallHeal = 3;

        // Skills
        public static int SwordSKillIDFastStrike = 0;
        public static int SwordSKillIDHeavyStrike = 1;
        public static int SwordSKillIDMultiHit = 2;

        // Items
        // Weapons
        public static int WeaponIDWoodStaff = 0;
        public static int WeaponIDWoodSword = 1;


        internal void AddRegion(string name, int id)
        {
            Region region = new Region
            {
                Name = name,
                ID = id
            };

            allRegions.Add(region);
        }

        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, Region region)
        {
            Location loc = new Location
            {
                XCoordinate = xCoordinate,
                YCoordinate = yCoordinate,
                Name = name,
                Description = description,
                Region = region
            };

            allLocations.Add(loc);
        }

        internal void AddMagic(string name, int id, string description, float spellDamage, int manaCost, float intelligenceModificator, Enum target)
        {
            Magic magic = new Magic
            {
                Name = name,
                ID = id,
                Description = description,
                BaseDamage = spellDamage,
                ManaCost = manaCost,
                IntelligenceModificator = intelligenceModificator,
                AffectedTarger = target
            };

            allMagicSpells.Add(magic);
        }

        internal void AddSkill(string name, int id, string description, float spellDamage, int manaCost, float strengthModificator, int numberOfHits, Enum target)
        {
            WeaponSkills skill = new WeaponSkills
            {
                Name = name,
                ID = id,
                Description = description,
                BaseDamage = spellDamage,
                ManaCost = manaCost,
                StrengthModificator = strengthModificator,
                NumberOfHits = numberOfHits,
                AffectedTarger = target
            };

            allSkills.Add(skill);
        }

        internal void AddRace(string name, int strength, int agility, int vitality, int intelligence, int mind, int luck, int id)
        {
            Race race = new Race
            {
                Name = name,
                Strength = strength,
                Agility = agility,
                Vitality = vitality,
                Intelligence = intelligence,
                Mind = mind,
                Luck = luck,
                ID = id,

            };

            allRaces.Add(race);
        }

        public Region RegionByID(int id)
        {
            foreach (var region in allRegions)
            {
                if (region.ID == id)
                {
                    return region;
                }
            }
            return null;
        }


        public static Race RaceByID(int id)
        {
            foreach (var race in allRaces)
            {
                if (race.ID == id)
                {
                    return race;
                }
            }
            return null;
        }

        public static Magic SpellByID(int id)
        {
            foreach (var spell in allMagicSpells)
            {
                if (spell.ID == id)
                {
                    return spell;
                }
            }
            return null;
        }

        public static WeaponSkills SkillByID(int id)
        {
            foreach (var skill in allSkills)
            {
                if (skill.ID == id)
                {
                    return skill;
                }
            }
            return null;
        }

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in allLocations)
            {
                if (loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }
            }
            return null;
        }
    }
}