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
            // Animal enemies only drop items (like skins, fangs etc). Gold only from humanoid enemies
            switch (enemyID)
            {
                case 0:
                    Enemy snake =
                        new Enemy("Snake", 1, 3, 3, 3, 1, 1, 3, 10, 0, 65, World.EnemyIDSnake, true);
                    return snake;
                case 1:
                    Enemy rat =
                        new Enemy("Rat", 1, 2, 5, 3, 1, 1, 4, 5, 0, 35, World.EnemyIDRat, false);
                    return rat;
                case 2:
                    Enemy goblin = 
                        new Enemy("Goblin", 2, 5, 4, 4, 1, 1, 3, 10, 10, 45, World.EnemyIDGoblin, true);
                    return goblin;
                case 3:
                    Enemy wolf =
                        new Enemy("Wolf", 2, 8, 8, 4, 1, 1, 3, 20, 0, 55, World.EnemyIDWolf, true);
                    return wolf;
                case 4:
                    Enemy rogue =
                        new Enemy("Rogue", 3, 6, 5, 6, 1, 1, 5, 30, 20, 35, World.EnemyIDRogue, true);
                    return rogue;
                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", enemyID));
            }
        }
    }
}
