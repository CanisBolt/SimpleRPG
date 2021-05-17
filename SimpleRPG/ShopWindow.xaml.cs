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
            if (gameSession.Hero.Gold >= selectedInventoryItem.BuyPrice)
            {
                gameSession.Hero.Gold -= selectedInventoryItem.BuyPrice;
                gameSession.Hero.AddItemToInventory(selectedInventoryItem);
                tbLog.Text = $"You bought {selectedInventoryItem.Name} for {selectedInventoryItem.BuyPrice} gold!"; // TODO add price * 2 to data grid
            }
            else
            {
                MessageBox.Show($"Not enough gold to buy {selectedInventoryItem.Name}.");
            }
        }

        private void SellItem(object sender, MouseButtonEventArgs e)
        {
            Game.Items.GameItems selectedInventoryItem = (Game.Items.GameItems)dbInventory.SelectedItem;
            if(selectedInventoryItem.SellPrice == 0)
            {
                MessageBox.Show("This item cannot be sold!");
                return;
            }
            else
            {
                gameSession.Hero.Gold += selectedInventoryItem.SellPrice;
                gameSession.Hero.RemoveItemToInventory(selectedInventoryItem);
                tbLog.Text = $"You sold {selectedInventoryItem.Name} and got {selectedInventoryItem.SellPrice} gold!";
            }
        }
    }
}
