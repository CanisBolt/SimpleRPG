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
    /// Логика взаимодействия для GardenWindow.xaml
    /// </summary>
    public partial class GardenWindow : Window
    {
        GameSession gameSession;
        public GardenWindow(GameSession _gameSession)
        {
            InitializeComponent();

            gameSession = _gameSession;


            UpdateGarden();
        }

        private void UpdateGarden()
        {
            foreach (var slot in gameSession.Hero.PlayersGarden.Slots)
            {
                if (slot == null)
                {
                    dbGarden.Items.Add(new Game.Items.GameItems("Empty Slot", -1, 0, 0, Game.Items.GameItems.TypeOfItem.Seed));
                }
                else
                {

                }
            }

            foreach(var item in gameSession.Hero.Inventory)
            {
                if(item.ItemType.Equals(Game.Items.GameItems.TypeOfItem.Seed))
                {
                    cbSeeds.Items.Add(item);
                }
            }
        }

        private void btnPlant_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
