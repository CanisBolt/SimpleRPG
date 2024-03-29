﻿using Game.LivingCreatures;
using System;

namespace Game.Factory
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
                        new Enemy("Snake", 1, 5, 3, 3, 1, 1, 3, 10, 0, 65, World.EnemyIDSnake, false, 1);
                    snake.Avatar = @"/Images/Creatures/Enemies/Village/snake.png";
                    snake.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDSnakeSkin));
                    snake.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDSnakeFang));
                    snake.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDSnakeEye));
                    return snake;
                case 1:
                    Enemy rat =
                        new Enemy("Rat", 1, 4, 5, 3, 1, 1, 4, 5, 0, 35, World.EnemyIDRat, false, 1);
                    rat.Avatar = @"/Images/Creatures/Enemies/Village/rat.png";
                    rat.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDRatSkin));
                    rat.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDRatTail));
                    return rat;
                case 2:
                    Enemy goblin =
                        new Enemy("Goblin", 2, 7, 4, 4, 1, 1, 3, 10, 10, 45, World.EnemyIDGoblin, true, 3);
                    goblin.Avatar = @"/Images/Creatures/Enemies/Forest/goblin.png";
                    goblin.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDGoblinSkin));
                    goblin.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDGoblinFang));
                    return goblin;
                case 3:
                    Enemy wolf =
                        new Enemy("Wolf", 2, 8, 8, 4, 1, 1, 3, 20, 0, 55, World.EnemyIDWolf, true, 5);
                    wolf.Avatar = @"/Images/Creatures/Enemies/Forest/wolf.png";
                    wolf.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDWolfSkin));
                    wolf.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDWolfFang));
                    return wolf;
                case 4:
                    Enemy rogue =
                        new Enemy("Rogue", 3, 6, 5, 6, 1, 2, 5, 30, 20, 35, World.EnemyIDRogue, true, 5);
                    rogue.Avatar = @"/Images/Creatures/Enemies/Forest/rogue.png";
                    rogue.SkillBook.Add(World.SkillByID(World.SwordSKillIDHeavyStrike));
                    return rogue;
                case 5:
                    Enemy forestWisp =
                        new Enemy("Forest Wisp", 3, 5, 3, 5, 10, 7, 5, 30, 0, 25, 5, false, 3);
                    forestWisp.Avatar = @"/Images/Creatures/Enemies/Forest/forestWisp.png";
                    forestWisp.SkillBook.Add(World.SkillByID(World.MagicIDFireball));
                    forestWisp.Inventory.Add(ItemsFactory.CreateGameItem(World.EnemyLootIDWispDust));
                    return forestWisp;
                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", enemyID));
            }
        }
    }
}
