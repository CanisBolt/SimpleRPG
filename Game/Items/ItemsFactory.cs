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

            CreateConsumable("Small Healing Potion", World.ItemIDSmallHealingPotion, 10, 0.25f);
            CreateConsumable("Medium Healing Potion", World.ItemIDMediumHealingPotion, 30, 0.5f);
            CreateConsumable("Big Healing Potion", World.ItemIDBigHealingPotion, 50, 0.75f);
            CreateConsumable("Max Healing Potion", World.ItemIDMaxHealingPotion, 100, 1f);

            CreateConsumable("Small Mana Potion", World.ItemIDSmallManaPotion, 10, 0.25f);
            CreateConsumable("Medium Mana Potion", World.ItemIDMediumManaPotion, 30, 0.5f);
            CreateConsumable("Big Mana Potion", World.ItemIDBigManaPotion, 50, 0.75f);
            CreateConsumable("Max Mana Potion", World.ItemIDMaxManaPotion, 100, 1f);
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

        private static void CreateConsumable(string name, int id, int price, float recoveryAmount)
        {
            standardGameItems.Add(new GameItems(name, id, price, GameItems.ItemType.Consumable, recoveryAmount));
        }
    }
}
