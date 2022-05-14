using System;

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
        public int NumberOfDices { get; set; }
        public int NumberOfSides { get; set; }
        public Enum TypeOfWeapon { get; set; }

        // Armor
        public int Defence { get; set; }
        public Enum ArmorSlot { get; set; }

        // Healing items
        public int RecoveryAmount { get; set; }

        // Basic Constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int quantity)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemType = type;
            Quantity = quantity;
        }

        // Weapon constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int quantity, int numberOfDices, int numberOfSides, Enum typeOfWeapon)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemType = type;
            NumberOfDices = numberOfDices;
            NumberOfSides = numberOfSides;
            TypeOfWeapon = typeOfWeapon;
            Quantity = quantity;
        }

        // Armor constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int quantity, int defence, Enum armorSlot)
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
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int quantity, int recoveryAmount)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemType = type;
            RecoveryAmount = recoveryAmount;
            Quantity = quantity;
        }

        public GameItems Clone()
        {
            switch (ItemType)
            {
                case TypeOfItem.Weapon:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, Quantity, NumberOfDices, NumberOfSides, TypeOfWeapon);
                case TypeOfItem.Armor:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, Quantity, Defence, ArmorSlot);
                case TypeOfItem.Consumable:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, Quantity, RecoveryAmount);
                case TypeOfItem.EnemyLoot:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, Quantity);
                default:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, Quantity);
            }
        }

        public enum TypeOfItem
        {
            Weapon,
            Armor,
            Consumable,
            Material,
            EnemyLoot,
            Miscellaneous,
            Seed
        }

        public enum WeaponType
        {
            None,
            Sword,
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
