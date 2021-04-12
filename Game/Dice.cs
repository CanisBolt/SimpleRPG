using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Dice
    {
        public static Random rng = new Random();
        public static int NumberOfSides { get; set; }
        public static int NumberOfDices { get; set; }

        public static int RollDice(int numberOfSides, int numberOfDices)
        {
            int number = 0;
            for(int i = 0; i < numberOfDices; i++)
            {
                number += rng.Next(numberOfSides) + 1;
            }
            return number;
        }
    }
}
