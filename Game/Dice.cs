using System;

namespace Game
{
    public static class Dice
    {
        public static Random rng = new Random();

        public static float GetRandomModificator()
        {
            return rng.Next(2, 4) * 0.4f; // TODO chance this to RNG Float
        }
    }
}
