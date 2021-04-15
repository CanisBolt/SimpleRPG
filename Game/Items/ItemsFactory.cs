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

            standardGameItems.Add(new Weapons("Wood Staff", 1, 1, 1, 2));
            standardGameItems.Add(new Weapons("Wood Sword", 2, 1, 2, 2));
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
    }
}
