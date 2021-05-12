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
        public int SellPrice { get; set; }
        public int BuyPrice { get; set; }
        public Enum Type { get; set; }
        public int Amount { get; set; } // TODO add stack items

        // Weapon
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public Enum TypeOfWeapon { get; set; }

        // Armor
        public int Defence { get; set; }
        public Enum ArmorSlot { get; set; }

        // Healing items
        public float RecoveryAmount { get; set; } // % from Hero MaxHP or MP

        // Enemy Loot
        public int DropChance { get; set; }

        // Basic Constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            Type = type;
        }

        // Weapon constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int minDamage, int maxDamage, Enum typeOfWeapon)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            Type = type;
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
            TypeOfWeapon = typeOfWeapon;
        }

        // Armor constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int defence, Enum armorSlot)
        {
            Name = name;
            ID = id; 
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            Type = type;
            Defence = defence;
            ArmorSlot = armorSlot;
        }

        // Consumable Constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, float recoveryAmount)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            Type = type;
            RecoveryAmount = recoveryAmount;
        }

        // EnemyLoot Constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int dropChance)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            Type = type;
            DropChance = dropChance;
        }

        public GameItems Clone()
        {
            if (Type.Equals(ItemType.Weapon)) return new GameItems(Name, ID, BuyPrice, SellPrice, Type, MinimumDamage, MaximumDamage, TypeOfWeapon); // Cloning weapon
            if (Type.Equals(ItemType.Armor)) return new GameItems(Name, ID, BuyPrice, SellPrice, Type, Defence, ArmorSlot); // Cloning armor
            if (Type.Equals(ItemType.Consumable)) return new GameItems(Name, ID, BuyPrice, SellPrice, Type, RecoveryAmount);
            if (Type.Equals(ItemType.Junk)) return new GameItems(Name, ID, BuyPrice, SellPrice, Type, DropChance);
            return new GameItems(Name, ID, BuyPrice, SellPrice, Type);
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
            None,
            Sword,
            Axe,
            Spear,
            Dagger,
            Staff
        }

        public enum ArmorType
        {
            Head,
            Body,
            Legs,
            Feet
        }
    }
}
