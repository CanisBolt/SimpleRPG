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
        public float RecoveryAmount { get; set; } // % from Hero MaxHP or MP

        // Enemy Loot
        public int DropChance { get; set; }

        // Seeds
        public int TimeToGrow { get; set; } // in seconds
        public int HarvestPlantID { get; set; }

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
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int numberOfDices, int numberOfSides, Enum typeOfWeapon, int quantity = 1)
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

        // Seeds Constructor
        public GameItems(string name, int id, int buyPrice, int sellPrice, Enum type, int timeToGrow, int harvestPlantID, int quantity = 1)
        {
            Name = name;
            ID = id;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemType = type;
            TimeToGrow = timeToGrow;
            HarvestPlantID = harvestPlantID;
            Quantity = quantity;
        }

        public GameItems Clone()
        {
            switch (ItemType)
            {
                case TypeOfItem.Weapon:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, NumberOfDices, NumberOfSides, TypeOfWeapon);
                case TypeOfItem.Armor:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, Defence, ArmorSlot);
                case TypeOfItem.Consumable:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, RecoveryAmount);
                case TypeOfItem.EnemyLoot:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, DropChance, Quantity);
                case TypeOfItem.Seed:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType, TimeToGrow, HarvestPlantID, Quantity);
                default:
                    return new GameItems(Name, ID, BuyPrice, SellPrice, ItemType);
            }
        }

        public enum TypeOfItem
        {
            Weapon,
            Armor,
            Consumable,
            Material,
            EnemyLoot,
            Seed
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
