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
        public Enum TypeOfWeapon { get; set; }
        public GameItems(string name, int id, int price, Enum type)
        {
            Name = name;
            ID = id;
            Price = price;
            Type = type;
        }

        // Weapon constructor
        public GameItems(string name, int id, int price, Enum type, int minDamage, int maxDamage, Enum typeOfWeapon)
        {
            Name = name;
            ID = id;
            Price = price;
            Type = type;
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
            TypeOfWeapon = typeOfWeapon;
        }

        public GameItems Clone()
        {
            if (TypeOfWeapon != null) return new GameItems(Name, ID, Price, Type, MinimumDamage, MaximumDamage, TypeOfWeapon); // Cloning weapon

            return new GameItems(Name, ID, Price, Type);
        }

        public enum ItemType
        {
            Weapon,
            Armor,
            Consumable,
            Junk
        }

        public enum WeaponType
        {
            Sword,
            Axe,
            Spear,
            Dagger,
            Staff
        }
    }
}
