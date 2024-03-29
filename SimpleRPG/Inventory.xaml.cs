﻿using Game;
using System.Windows;
using System.Windows.Input;

namespace SimpleRPG
{
    /// <summary>
    /// Логика взаимодействия для Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        GameSession gameSession;
        bool IsInBattle; // Special variable that check if window was opened in Battle
        public bool IsItemUsed { get; set; } // For battle. Enemy turn after closing inventory
        public Inventory(GameSession _gameSession, bool isInBattle)
        {
            InitializeComponent();
            gameSession = _gameSession;
            DataContext = gameSession;
            IsInBattle = isInBattle;

            IsItemUsed = false;
        }

        private void UseItem(object sender, MouseButtonEventArgs e)
        {
            Game.Items.GameItems selectedInventoryItem = (Game.Items.GameItems)dbInventory.SelectedItem;
            if (selectedInventoryItem.ItemType.Equals(Game.Items.GameItems.TypeOfItem.Consumable))
            {
                if (selectedInventoryItem.Name.Contains("Healing"))
                {
                    if (gameSession.Hero.CurrentHP == gameSession.Hero.MaxHP)
                    {
                        MessageBox.Show("Your HP is Full");
                        return;
                    }
                    gameSession.Hero.CurrentHP += (int)(gameSession.Hero.MaxHP * selectedInventoryItem.RecoveryAmount);
                    if (gameSession.Hero.CurrentHP >= gameSession.Hero.MaxHP) gameSession.Hero.CurrentHP = gameSession.Hero.MaxHP;
                }
                else if (selectedInventoryItem.Name.Contains("Mana"))
                {
                    if (gameSession.Hero.CurrentMP == gameSession.Hero.MaxMP)
                    {
                        MessageBox.Show("Your MP is Full");
                        return;
                    }
                    gameSession.Hero.CurrentMP += (int)(gameSession.Hero.MaxMP * selectedInventoryItem.RecoveryAmount);
                    if (gameSession.Hero.CurrentMP >= gameSession.Hero.MaxMP) gameSession.Hero.CurrentMP = gameSession.Hero.MaxMP;
                }
                gameSession.Hero.RemoveItemToInventory(selectedInventoryItem);

                IsItemUsed = true;
            }

            else if (selectedInventoryItem.ItemType.Equals(Game.Items.GameItems.TypeOfItem.Weapon))
            {
                if (gameSession.Hero.CurrentWeapon != (Game.Items.GameItems)dbInventory.SelectedItem)
                {
                    gameSession.Hero.AddItemToInventory(gameSession.Hero.CurrentWeapon);
                    gameSession.Hero.CurrentWeapon = (Game.Items.GameItems)dbInventory.SelectedItem;
                    gameSession.Hero.RemoveItemToInventory(gameSession.Hero.CurrentWeapon);
                }

                IsItemUsed = true;
            }

            else if (selectedInventoryItem.ItemType.Equals(Game.Items.GameItems.TypeOfItem.Armor))
            {
                switch (selectedInventoryItem.ArmorSlot)
                {
                    case Game.Items.GameItems.ArmorType.Head:
                        if (gameSession.Hero.CurrentHeadArmor != (Game.Items.GameItems)dbInventory.SelectedItem)
                        {
                            gameSession.Hero.Inventory.Add(gameSession.Hero.CurrentHeadArmor);
                            gameSession.Hero.CurrentHeadArmor = (Game.Items.GameItems)dbInventory.SelectedItem;
                            gameSession.Hero.Inventory.Remove(gameSession.Hero.CurrentHeadArmor);
                        }
                        break;
                    case Game.Items.GameItems.ArmorType.Body:
                        if (gameSession.Hero.CurrentBodyArmor != (Game.Items.GameItems)dbInventory.SelectedItem)
                        {
                            gameSession.Hero.Inventory.Add(gameSession.Hero.CurrentBodyArmor);
                            gameSession.Hero.CurrentBodyArmor = (Game.Items.GameItems)dbInventory.SelectedItem;
                            gameSession.Hero.Inventory.Remove(gameSession.Hero.CurrentBodyArmor);
                        }
                        break;
                    case Game.Items.GameItems.ArmorType.Legs:
                        if (gameSession.Hero.CurrentLegsArmor != (Game.Items.GameItems)dbInventory.SelectedItem)
                        {
                            gameSession.Hero.Inventory.Add(gameSession.Hero.CurrentLegsArmor);
                            gameSession.Hero.CurrentLegsArmor = (Game.Items.GameItems)dbInventory.SelectedItem;
                            gameSession.Hero.Inventory.Remove(gameSession.Hero.CurrentLegsArmor);
                        }
                        break;
                    case Game.Items.GameItems.ArmorType.Feet:
                        if (gameSession.Hero.CurrentFeetArmor != (Game.Items.GameItems)dbInventory.SelectedItem)
                        {
                            gameSession.Hero.Inventory.Add(gameSession.Hero.CurrentFeetArmor);
                            gameSession.Hero.CurrentFeetArmor = (Game.Items.GameItems)dbInventory.SelectedItem;
                            gameSession.Hero.Inventory.Remove(gameSession.Hero.CurrentFeetArmor);
                        }
                        break;
                }
            }

            if (IsItemUsed && IsInBattle)
            {
                Close();
            }
        }

    }
}
