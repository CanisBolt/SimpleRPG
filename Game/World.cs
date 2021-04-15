using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.LivingCreatures;

namespace Game
{
    public class World
    {
        private static List<Race> allRaces = new List<Race>();
        private List<Location> locations = new List<Location>();

        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description)
        {
            Location loc = new Location
            {
                XCoordinate = xCoordinate,
                YCoordinate = yCoordinate,
                Name = name,
                Description = description
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