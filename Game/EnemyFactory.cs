using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.LivingCreatures;

namespace Game
{
    public class EnemyFactory
    {
        public static Enemy GetMonster(int enemyID)
        {
            switch (enemyID)
            {
                case 0:
                    Enemy snake =
                        new Enemy("Snake", 1, 2, 1, 2, 1, 1, 1, 1, 1, 55, World.EnemyIDSnake, true);
                    return snake;
                case 1:
                    Enemy rat =
                        new Enemy("Rat", 1, 1, 2, 2, 1, 1, 2, 1, 1, 35, World.EnemyIDRat, false);
                    return rat;
                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", enemyID));
            }
        }
    }
}
