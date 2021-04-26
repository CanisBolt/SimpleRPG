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
                        new Enemy("Snake", 1, 3, 3, 3, 1, 1, 3, 5, 5, 55, World.EnemyIDSnake, true);
                    return snake;
                case 1:
                    Enemy rat =
                        new Enemy("Rat", 1, 2, 5, 3, 1, 1, 4, 5, 5, 35, World.EnemyIDRat, false);
                    return rat;
                case 2:
                    Enemy goblin = 
                        new Enemy("Goblin", 2, 5, 4, 4, 1, 1, 3, 10, 10, 45, World.EnemyIDGoblin, true);
                    return goblin;
                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", enemyID));
            }
        }
    }
}
