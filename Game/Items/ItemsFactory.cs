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

            CreateWeapon("Wood Staff", World.WeaponIDWoodStaff, 1, 1, 2);
            CreateWeapon("Wood Sword", World.WeaponIDWoodSword, 1, 2, 2);
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

        private static void CreateWeapon(string name, int id, int price, int minDamage, int maxDamage)
        {
            standardGameItems.Add(new GameItems(name, id, price, GameItems.ItemType.Weapon, minDamage, maxDamage));
        }
    }
}
