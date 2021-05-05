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

        // Weapon
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public Enum TypeOfWeapon { get; set; }

        // Healing items
        public float RecoveryAmount { get; set; } // % from Hero MaxHP or MP
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

        public GameItems(string name, int id, int price, Enum type, float recoveryAmount)
        {
            Name = name;
            ID = id;
            Price = price;
            Type = type;
            RecoveryAmount = recoveryAmount;
        }

        public GameItems Clone()
        {
            if (Type.Equals(ItemType.Weapon)) return new GameItems(Name, ID, Price, Type, MinimumDamage, MaximumDamage, TypeOfWeapon); // Cloning weapon
            if (Type.Equals(ItemType.Consumable)) return new GameItems(Name, ID, Price, Type, RecoveryAmount);
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
