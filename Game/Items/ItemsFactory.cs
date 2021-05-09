using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Items
{
    public class ItemsFactory
    {
        private static List<GameItems> standardGameItems;

        static ItemsFactory()
        {
            standardGameItems = new List<GameItems>();

            CreateWeapon("Wood Staff", World.WeaponIDWoodStaff, 1, 1, 2, GameItems.WeaponType.Staff);
            CreateWeapon("Wood Sword", World.WeaponIDWoodSword, 1, 2, 2, GameItems.WeaponType.Sword);

            CreateArmor("No Head Armor", World.ArmorIDNoHeadArmor, 0, 0, GameItems.ArmorType.Head);
            CreateArmor("No Body Armor", World.ArmorIDNoBodyArmor, 0, 0, GameItems.ArmorType.Body);
            CreateArmor("No Legs Armor", World.ArmorIDNoLegsArmor, 0, 0, GameItems.ArmorType.Legs);
            CreateArmor("No Feet Armor", World.ArmorIDNoFeetArmor, 0, 0, GameItems.ArmorType.Feet);

            CreateArmor("Silk Hat", World.ArmorIDSilkHat, 1, 1, GameItems.ArmorType.Head);
            CreateArmor("Silk Robe", World.ArmorIDSilkRobe, 1, 1, GameItems.ArmorType.Body);
            CreateArmor("Silk Pants", World.ArmorIDSilkPants, 1, 1, GameItems.ArmorType.Legs);
            CreateArmor("Silk Sandals", World.ArmorIDSilkSandals, 1, 1, GameItems.ArmorType.Feet);

            CreateConsumable("Small Healing Potion", World.ItemIDSmallHealingPotion, 10, 0.25f);
            CreateConsumable("Medium Healing Potion", World.ItemIDMediumHealingPotion, 30, 0.5f);
            CreateConsumable("Big Healing Potion", World.ItemIDBigHealingPotion, 50, 0.75f);
            CreateConsumable("Max Healing Potion", World.ItemIDMaxHealingPotion, 100, 1f);

            CreateConsumable("Small Mana Potion", World.ItemIDSmallManaPotion, 10, 0.25f);
            CreateConsumable("Medium Mana Potion", World.ItemIDMediumManaPotion, 30, 0.5f);
            CreateConsumable("Big Mana Potion", World.ItemIDBigManaPotion, 50, 0.75f);
            CreateConsumable("Max Mana Potion", World.ItemIDMaxManaPotion, 100, 1f);

            CreateJunkItem("Snake Skin", World.EnemyLootIDSnakeSkin, 2, 50);
            CreateJunkItem("Snake Fang", World.EnemyLootIDSnakeFang, 5, 25);
            CreateJunkItem("Snake Eye", World.EnemyLootIDSnakeEye, 10, 10);
            CreateJunkItem("Rat Skin", World.EnemyLootIDRatSkin, 1, 80);
            CreateJunkItem("Rat Tail", World.EnemyLootIDRatTail, 2, 70);
            CreateJunkItem("Goblin Skin", World.EnemyLootIDGoblinSkin, 12, 30);
            CreateJunkItem("Goblin Fang", World.EnemyLootIDGoblinFang, 15, 25);
            CreateJunkItem("Wolf Skin", World.EnemyLootIDWolfSkin, 15, 30);
            CreateJunkItem("Wolf Fanf", World.EnemyLootIDWolfFang, 17, 20);
            CreateJunkItem("Wisp Dust", World.EnemyLootIDWispDust, 20, 10);
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

        private static void CreateWeapon(string name, int id, int price, int minDamage, int maxDamage, Enum weaponType)
        {
            standardGameItems.Add(new GameItems(name, id, price, GameItems.ItemType.Weapon, minDamage, maxDamage, weaponType));
        }

        private static void CreateArmor(string name, int id, int price, int defence, Enum ArmorSlot)
        {
            standardGameItems.Add(new GameItems(name, id, price, GameItems.ItemType.Armor, defence, ArmorSlot));
        }

        private static void CreateConsumable(string name, int id, int price, float recoveryAmount)
        {
            standardGameItems.Add(new GameItems(name, id, price, GameItems.ItemType.Consumable, recoveryAmount));
        }

        private static void CreateJunkItem(string name, int id, int price, int dropChance)
        {
            standardGameItems.Add(new GameItems(name, id, price, GameItems.ItemType.Junk, dropChance));
        }
    }
}
