using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Game;

namespace SimpleRPG
{
    /// <summary>
    /// Логика взаимодействия для ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        GameSession gameSession;
        public ShopWindow(GameSession _gameSession)
        {
            InitializeComponent(); 
            gameSession = _gameSession;
            DataContext = gameSession;
        }

        private void BuyItem(object sender, MouseButtonEventArgs e)
        {
            Game.Items.GameItems selectedInventoryItem = (Game.Items.GameItems)dbShopInventory.SelectedItem;
            if (gameSession.Hero.Gold >= selectedInventoryItem.Price * 2)
            {
                gameSession.Hero.Gold -= selectedInventoryItem.Price * 2;
                gameSession.Hero.Inventory.Add(selectedInventoryItem);
                tbLog.Text = $"You bought {selectedInventoryItem.Name} for {selectedInventoryItem.Price * 2} gold!"; // TODO add price * 2 to data grid
            }
            else
            {
                MessageBox.Show($"Not enough gold to buy {selectedInventoryItem.Name}.");
            }
        }

        private void SellItem(object sender, MouseButtonEventArgs e)
        {
            Game.Items.GameItems selectedInventoryItem = (Game.Items.GameItems)dbInventory.SelectedItem;
            if (selectedInventoryItem.Type.Equals(Game.Items.GameItems.ItemType.Weapon) && gameSession.Hero.CurrentWeapon.Equals(selectedInventoryItem))
            {
                MessageBox.Show("You cannot sell equiped item!");
            }
            else
            {
                gameSession.Hero.Gold += selectedInventoryItem.Price;
                gameSession.Hero.Inventory.Remove(selectedInventoryItem);
                tbLog.Text = $"You sold {selectedInventoryItem.Name} and got {selectedInventoryItem.Price} gold!";
            }
        }
    }
}
