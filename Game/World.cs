using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class World
    {
        public static Random rng = new Random();
        public static List<Race> allRaces = new List<Race>();



        // Races ID
        public static int RaceHumanID = 0;
        public static int RaceElfID = 1;
        public static int RaceDwarfID = 2;
        public static int RaceDogFolkID = 3;
        public static int RaceCatFolkID = 4;

        static World()
        {
            CreateRaces();
        }


        private static void CreateRaces()
        {
            Race human = new Race("Human", 0, 0, 0, 0, 0, 0, RaceHumanID);
            Race elf = new Race("Elf", -2, 2, -2, 1, 0, 1, RaceElfID);
            Race dwarf = new Race("Dwarf", 2, -2, 2, -2, 0, 0, RaceDwarfID);
            Race dogFolk = new Race("DogFolk", -1, -2, 0, 2, 2, -1, RaceDogFolkID);
            Race catFolk = new Race("CatFolk", -2, 2, -1, 0, -1, 2, RaceCatFolkID);

            // TODO add race descriptions

            allRaces.Add(human);
            allRaces.Add(elf);
            allRaces.Add(dwarf);
            allRaces.Add(dogFolk);
            allRaces.Add(catFolk);
        }

        public static Race RaceByID(int id)
        {
            foreach(var race in allRaces)
            {
                if(race.ID ==id)
                {
                    return race;
                }
            }
            return null;
        }
    }
}
