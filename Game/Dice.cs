using System;

namespace Game
{
    public static class Dice
    {
        public static Random rng = new Random();

        // Each attack has small variation in damage
        public static float GetRandomModificator()
        {
            return rng.Next(2, 4) * 0.4f;
        }

        // Roll different type of dices (like 3 side dice, 4 side, 6 side etc)
        public static float RollDice(int numberOfDices, int numberOfSides)
        {
            int result = 0;

            for(int i = 0; i < numberOfDices; i++)
            {
                result += rng.Next(1, numberOfSides + 1);
            }

            return result;
        }
    }
}
