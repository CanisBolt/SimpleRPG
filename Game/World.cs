using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.GameLocations;
using Game.LivingCreatures;

namespace Game
{
    public class World
    {
        private static List<Race> allRaces = new List<Race>();
        private List<Location> locations = new List<Location>();
        private List<Region> regions = new List<Region>();

        // ID's

        // Races
        public static int RaceIDHuman = 0;
        public static int RaceIDElf = 1;
        public static int RaceIDDwarf = 2;
        public static int RaceIDDogFolk = 3;
        public static int RaceIDCatFolk = 4;

        // Regions
        public static int RegionIDVillage = 0;

        // Enemies
        public static int EnemyIDSnake = 0;
        public static int EnemyIDRat = 1;

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

            regions.Add(region);
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

            locations.Add(loc);
        }

        internal void CreateRaces(string name, int strength, int agility, int vitality, int intelligence, int mind, int luck, int id)
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
            foreach (var region in regions)
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

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in locations)
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