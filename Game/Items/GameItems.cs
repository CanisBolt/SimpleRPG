using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Items
{
    public class GameItems
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Price { get; set; }
        public Enum Type { get; set; }

        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public GameItems(string name, int id, int price, Enum type)
        {
            Name = name;
            ID = id;
            Price = price;
            Type = type;
        }

        // Weapon constructor
        public GameItems(string name, int id, int price, Enum type, int minDamage, int maxDamage)
        {
            Name = name;
            ID = id;
            Price = price;
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
            Type = type;
        }

        public GameItems Clone()
        {
            return new GameItems(Name, ID, Price, Type);
        }

        public enum ItemType
        {
            Weapon,
            Armor,
            Consumable,
            Junk
        }
    }
}
