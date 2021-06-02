using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Items
{
    public class ItemsFactory
    {
        private static List<GameItems> standardGameItems;

        static ItemsFactory()
        {
            standardGameItems = new List<GameItems>();

            CreateWeapon("Wood Staff", World.WeaponIDWoodStaff, 2, 1, 1, 2, GameItems.WeaponType.Staff);
            CreateWeapon("Wood Sword", World.WeaponIDWoodSword, 2, 1, 2, 2, GameItems.WeaponType.Sword);

            CreateArmor("No Head Armor", World.ArmorIDNoHeadArmor, 0, 0, 0, GameItems.ArmorType.Head);
            CreateArmor("No Body Armor", World.ArmorIDNoBodyArmor, 0, 0, 0, GameItems.ArmorType.Body);
            CreateArmor("No Legs Armor", World.ArmorIDNoLegsArmor, 0, 0, 0, GameItems.ArmorType.Legs);
            CreateArmor("No Feet Armor", World.ArmorIDNoFeetArmor, 0, 0, 0, GameItems.ArmorType.Feet);

            CreateArmor("Silk Hat", World.ArmorIDSilkHat, 2, 1, 1, GameItems.ArmorType.Head);
            CreateArmor("Silk Robe", World.ArmorIDSilkRobe, 2, 1, 1, GameItems.ArmorType.Body);
            CreateArmor("Silk Pants", World.ArmorIDSilkPants, 2, 1, 1, GameItems.ArmorType.Legs);
            CreateArmor("Silk Sandals", World.ArmorIDSilkSandals, 2, 1, 1, GameItems.ArmorType.Feet);

            CreateConsumable("Small Healing Potion", World.ItemIDSmallHealingPotion, 20, 10, 0.25f);
            CreateConsumable("Medium Healing Potion", World.ItemIDMediumHealingPotion, 60, 30, 0.5f);
            CreateConsumable("Big Healing Potion", World.ItemIDBigHealingPotion, 100, 50, 0.75f);
            CreateConsumable("Max Healing Potion", World.ItemIDMaxHealingPotion, 200, 100, 1f);

            CreateConsumable("Small Mana Potion", World.ItemIDSmallManaPotion, 20, 10, 0.25f);
            CreateConsumable("Medium Mana Potion", World.ItemIDMediumManaPotion, 60, 30, 0.5f);
            CreateConsumable("Big Mana Potion", World.ItemIDBigManaPotion, 100, 50, 0.75f);
            CreateConsumable("Max Mana Potion", World.ItemIDMaxManaPotion, 200, 100, 1f);

            CreateMaterial("Healing Grass", World.MaterialIDHealingGrass, 0, 2);

            CreateEnemyLootItem("Snake Skin", World.EnemyLootIDSnakeSkin, 0, 2, 50, 1);
            CreateEnemyLootItem("Snake Fang", World.EnemyLootIDSnakeFang, 0, 5, 25, 1);
            CreateEnemyLootItem("Snake Eye", World.EnemyLootIDSnakeEye, 0, 10, 10, 1);
            CreateEnemyLootItem("Rat Skin", World.EnemyLootIDRatSkin, 0, 1, 80, 1);
            CreateEnemyLootItem("Rat Tail", World.EnemyLootIDRatTail, 0, 2, 70, 1);
            CreateEnemyLootItem("Goblin Skin", World.EnemyLootIDGoblinSkin, 0, 12, 30, 1);
            CreateEnemyLootItem("Goblin Fang", World.EnemyLootIDGoblinFang, 0, 15, 25, 1);
            CreateEnemyLootItem("Wolf Skin", World.EnemyLootIDWolfSkin, 0, 15, 30, 1);
            CreateEnemyLootItem("Wolf Fanf", World.EnemyLootIDWolfFang, 0, 17, 20, 1);
            CreateEnemyLootItem("Wisp Dust", World.EnemyLootIDWispDust, 0, 20, 10, 1);

            CreateSeed("Healing Grass Seed", World.SeedIDHealingGrass, 5, 2, 120, World.MaterialIDHealingGrass, 1);
        }

        public static GameItems CreateGameItem(int itemID)
        {
            GameItems standardItem = standardGameItems.FirstOrDefault(item => item.ID == itemID);

            if (standardItem != null)
            {
                return standardItem.Clone();
            }

            return null;
        }

        private static void CreateWeapon(string name, int id, int buyPrice, int sellPrice, int minDamage, int maxDamage, Enum weaponType)
        {
            standardGameItems.Add(new GameItems(name, id, buyPrice, sellPrice, GameItems.TypeOfItem.Weapon, minDamage, maxDamage, weaponType));
        }

        private static void CreateArmor(string name, int id, int buyPrice, int sellPrice, int defence, Enum ArmorSlot)
        {
            standardGameItems.Add(new GameItems(name, id, buyPrice, sellPrice, GameItems.TypeOfItem.Armor, defence, ArmorSlot));
        }

        private static void CreateConsumable(string name, int id, int buyPrice, int sellPrice, float recoveryAmount)
        {
            standardGameItems.Add(new GameItems(name, id, buyPrice, sellPrice, GameItems.TypeOfItem.Consumable, recoveryAmount));
        }

        private static void CreateMaterial(string name, int id, int buyPrice, int sellPrice)
        {
            standardGameItems.Add(new GameItems(name, id, buyPrice, sellPrice, GameItems.TypeOfItem.Material));
        }

        private static void CreateEnemyLootItem(string name, int id, int buyPrice, int sellPrice, int dropChance, int quantity)
        {
            standardGameItems.Add(new GameItems(name, id, buyPrice, sellPrice, GameItems.TypeOfItem.EnemyLoot, dropChance, quantity));
        }

        private static void CreateSeed(string name, int id, int buyPrice, int sellPrice, int timeToGrow, int harvestPlant, int quantity)
        {
            standardGameItems.Add(new GameItems(name, id, buyPrice, sellPrice, GameItems.TypeOfItem.EnemyLoot, timeToGrow, harvestPlant, quantity));
        }

        public static GameItems ItemByID(int id)
        {
            foreach (var item in standardGameItems)
            {
                if (item.ID.Equals(id))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
