using Game.Items;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

namespace Game.Factory
{
    public static class ItemsFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameItems.xml";
        private static readonly List<GameItems> standardGameItems = new List<GameItems>();

        static ItemsFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadItemsFromNodes(data.SelectNodes("/GameItems/Weapons/Weapon"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Armors/Armor"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Consumables/Consumable"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Materials/Material"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Loot/EnemyLoot"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        public static GameItems CreateGameItem(int itemID)
        {
            return standardGameItems.FirstOrDefault(item => item.ID == itemID)?.Clone();
        }

        public static string ItemName(int itemID)
        {
            return standardGameItems.FirstOrDefault(i => i.ID == itemID)?.Name ?? "";
        }

        private static void LoadItemsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                GameItems.TypeOfItem itemCategory = DetermineItemCategory(node.Name);
                GameItems item = null;
                switch(itemCategory)
                {
                    case GameItems.TypeOfItem.Weapon:
                        item = new GameItems(node.AttributeAsString("Name"),
                            node.AttributeAsInt("ID"),
                            node.AttributeAsInt("BuyPrice"),
                            node.AttributeAsInt("SellPrice"),
                            itemCategory,
                            node.AttributeAsInt("Quantity"),
                            node.AttributeAsInt("NumberOfDices"),
                            node.AttributeAsInt("NumberOfSides"),
                            DetermineWeaponType(node.Name));
                        break;
                    case GameItems.TypeOfItem.Armor:
                        item = new GameItems(node.AttributeAsString("Name"),
                            node.AttributeAsInt("ID"),
                            node.AttributeAsInt("BuyPrice"),
                            node.AttributeAsInt("SellPrice"),
                            itemCategory,
                            node.AttributeAsInt("Quantity"),
                            node.AttributeAsInt("Defence"),
                            DetermineArmorSlot(node.Name));
                        break;
                    case GameItems.TypeOfItem.Consumable:
                        item = new GameItems(node.AttributeAsString("Name"),
                            node.AttributeAsInt("ID"),
                            node.AttributeAsInt("BuyPrice"),
                            node.AttributeAsInt("SellPrice"),
                            itemCategory,
                            node.AttributeAsInt("Quantity"),
                            node.AttributeAsInt("RecoveryAmount"));
                        break;
                    case GameItems.TypeOfItem.Material:
                        item = new GameItems(node.AttributeAsString("Name"),
                            node.AttributeAsInt("ID"),
                            node.AttributeAsInt("BuyPrice"),
                            node.AttributeAsInt("SellPrice"),
                            itemCategory,
                            node.AttributeAsInt("Quantity"));
                        break;
                    case GameItems.TypeOfItem.EnemyLoot:
                        item = new GameItems(node.AttributeAsString("Name"),
                            node.AttributeAsInt("ID"),
                            node.AttributeAsInt("BuyPrice"),
                            node.AttributeAsInt("SellPrice"),
                            itemCategory,
                            node.AttributeAsInt("Quantity"));
                        break;
                    case GameItems.TypeOfItem.Miscellaneous:
                        item = new GameItems(node.AttributeAsString("Name"),
                            node.AttributeAsInt("ID"),
                            node.AttributeAsInt("BuyPrice"),
                            node.AttributeAsInt("SellPrice"),
                            itemCategory,
                            node.AttributeAsInt("Quantity"));
                        break;
                }

                standardGameItems.Add(item);
            }
        }

        private static GameItems.TypeOfItem DetermineItemCategory(string itemType)
        {
            switch (itemType)
            {
                case "Weapon":
                    return GameItems.TypeOfItem.Weapon;
                case "Armor":
                    return GameItems.TypeOfItem.Armor;
                case "Consumable":
                    return GameItems.TypeOfItem.Consumable;
                case "Material":
                    return GameItems.TypeOfItem.Material;
                case "EnemyLoot":
                    return GameItems.TypeOfItem.EnemyLoot;
                default:
                    return GameItems.TypeOfItem.Miscellaneous;
            }
        }

        private static GameItems.WeaponType DetermineWeaponType(string itemType)
        {
            switch (itemType)
            {
                case "Sword":
                    return GameItems.WeaponType.Sword;
                case "Dagger":
                    return GameItems.WeaponType.Dagger;
                default:
                    return GameItems.WeaponType.Staff;
            }
        }

        private static GameItems.ArmorType DetermineArmorSlot(string itemType)
        {
            switch (itemType)
            {
                case "Head":
                    return GameItems.ArmorType.Head;
                case "Body":
                    return GameItems.ArmorType.Body;
                case "Legs":
                    return GameItems.ArmorType.Legs;
                default:
                    return GameItems.ArmorType.Feet;
            }
        }
    }
}
