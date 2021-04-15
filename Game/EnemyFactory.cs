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
        public static Enemy GetMonster(int monsterID)
        {
            switch (monsterID)
            {
                case 1:
                    Enemy snake =
                        new Enemy("Goblin", 1, 2, 1, 2, 1, 1, 1, 1, 1, 35, 1);
                    return snake;
                case 2:
                    Enemy rat =
                        new Enemy("Rat", 1, 1, 2, 2, 1, 1, 2, 1, 1, 35, 2);
                    return rat;
                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", monsterID));
            }
        }
    }
}
