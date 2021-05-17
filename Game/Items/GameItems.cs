using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Items
{
    public class GameItems : BaseNotificationClass
    {
        private int quantity;
        public string Name { get; set; }
        public int ID { get; set; }
        public int SellPrice { get; set; }
        public int BuyPrice { get; set; }
        public Enum ItemType { get; set; }
        public int Quantity 
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(quantity));
            }
        }

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
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int quantity = 1)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemType = type;
            Quantity = quantity;
        }

        // Weapon constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int minDamage, int maxDamage, Enum typeOfWeapon, int quantity = 1)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemType = type;
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
            TypeOfWeapon = typeOfWeapon;
            Quantity = quantity;
        }

        // Armor constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int defence, Enum armorSlot, int quantity = 1)
        {
            Name = name;
            ID = id; 
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemType = type;
            Defence = defence;
            ArmorSlot = armorSlot;
            Quantity = quantity;
        }

        // Consumable Constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, float recoveryAmount, int quantity = 1)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemType = type;
            RecoveryAmount = recoveryAmount;
            Quantity = quantity;
        }

        // EnemyLoot Constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int dropChance, int quantity = 1)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemType = type;
            DropChance = dropChance;
            Quantity = quantity;
        }

        public GameItems Clone()
        {
            switch(ItemType)
            {
                case TypeOfItem.Weapon:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, MinimumDamage, MaximumDamage, TypeOfWeapon);
                case TypeOfItem.Armor:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, Defence, ArmorSlot);
                case TypeOfItem.Consumable:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, RecoveryAmount);
                case TypeOfItem.EnemyLoot:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, DropChance, Quantity);
                default:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType);
            }
        }

        public enum TypeOfItem
        {
            Weapon,
            Armor,
            Consumable,
            EnemyLoot
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
