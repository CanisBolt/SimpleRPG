using Game.LivingCreatures;
using Game.Factory;
using System.Collections.Generic;
using System.Linq;

namespace Game.GameLocations
{
    public class Region
    {
        public string Name { get; set; }
        public int ID { get; set; }

        // Collect locations in region and attach monsters to region, not location itself

        public List<Enemy> EnemiesHere { get; set; } = new List<Enemy>();

        public void AddEnemy(Enemy enemy)
        {
            if (!EnemiesHere.Exists(e => e.ID == enemy.ID))
            {
                // if enemy is not in the list, add it to the list
                EnemiesHere.Add(new Enemy(enemy.Name, enemy.Level, enemy.Strength, enemy.Agility, enemy.Vitality, enemy.Intelligence, enemy.Mind, enemy.Luck, enemy.RewardEXP, enemy.RewardGold, enemy.EncounterChance, enemy.ID, enemy.IsAgressive, enemy.Defence));
            }
        }

        public Enemy GetEnemy()
        {
            if (!EnemiesHere.Any())
            {
                return null;
            }

            // add chance to not encounter the enemy
            if (Dice.rng.Next(20) < 5) return null;

            // Total encounter chance of all enemies at this region.
            int totalChanceToAppear = EnemiesHere.Sum(e => e.EncounterChance);

            int randomNumber = Dice.rng.Next(1, totalChanceToAppear);

            // Loop through the monster list, 
            // adding the enemies percentage chance of appearing to the runningTotal variable.
            // When the random number is lower than the runningTotal,
            // this enemy will be returned
            int runningTotal = 0;

            foreach (Enemy enemyEncounter in EnemiesHere)
            {
                runningTotal += enemyEncounter.EncounterChance;

                if (randomNumber <= runningTotal)
                {
                    return EnemyFactory.GetMonster(enemyEncounter.ID);
                }
            }

            // if there's a problem in foreach loop, return last enemy in the list
            return EnemyFactory.GetMonster(EnemiesHere.Last().ID);
        }
    }
}
