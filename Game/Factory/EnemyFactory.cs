using Game.LivingCreatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Game.Factory
{
    public class EnemyFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Enemies.xml";
        private static readonly List<Enemy> _baseEnemies = new List<Enemy>();

        static EnemyFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME)); 
                
                string rootImagePath =
                     data.SelectSingleNode("/Enemies")
                         .AttributeAsString("RootAvatarPath");

                LoadEnemiesFromNodes(data.SelectNodes("/Enemies/Enemy"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadEnemiesFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                Enemy enemy =
                    new Enemy(node.AttributeAsString("Name"),
                    node.AttributeAsInt("Level"),
                    node.AttributeAsInt("Strength"),
                    node.AttributeAsInt("Agility"),
                    node.AttributeAsInt("Vitality"),
                    node.AttributeAsInt("Intelligence"),
                    node.AttributeAsInt("Mind"),
                    node.AttributeAsInt("Luck"),
                    node.AttributeAsInt("RewardEXP"),
                    node.AttributeAsInt("RewardGold"),
                    node.AttributeAsInt("EncounterChance"),
                    node.AttributeAsInt("ID"),
                    node.AttributeAsBool("IsAgressive"),
                    node.AttributeAsInt("Defence"),
                    $".{rootImagePath}{node.AttributeAsString("Avatar")}");

                XmlNodeList lootItemNodes = node.SelectNodes("./Loots/EnemyLoot");
                if (lootItemNodes != null)
                {
                    foreach (XmlNode lootItemNode in lootItemNodes)
                    {
                        enemy.AddItemToLootTable(lootItemNode.AttributeAsInt("ID"),
                                                   lootItemNode.AttributeAsInt("Percentage"));
                    }
                }

                _baseEnemies.Add(enemy);
            }
        }

        public static Enemy GetEnemy(int id)
        {
            return _baseEnemies.FirstOrDefault(m => m.ID == id)?.GetNewInstance();
        }
    }
}
