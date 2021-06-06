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

            dbGarden.ItemsSource = gameSession.Hero.PlayersGarden.Slots;
            cbSeeds.ItemsSource = gameSession.Hero.PlayersGarden.AvailableSeeds;

            UpdateGarden();
        }

        private void UpdateGarden()
        {
            dbGarden.Items.Refresh();
            cbSeeds.Items.Refresh();
        }

        private async void Growing()
        {
            await Task.Run(async () =>
            {
                while(!gameSession.Hero.PlayersGarden.Slots.All(seed => seed.Name.Equals("Empty Slot")))
                {
                    for (int i = 0; i < gameSession.Hero.PlayersGarden.Size; i++)
                    {
                        if (gameSession.Hero.PlayersGarden.Slots[i].Name.Equals("Empty Slot")) continue;
                           
                        gameSession.Hero.PlayersGarden.Slots[i].TimeToGrow--;
                        if (gameSession.Hero.PlayersGarden.Slots[i].TimeToGrow == 0)
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                gameSession.Hero.AddItemToInventory(Game.Items.ItemsFactory.ItemByID(gameSession.Hero.PlayersGarden.Slots[i].HarvestPlantID));
                                gameSession.Hero.PlayersGarden.Slots[i] = new Game.Items.GameItems("Empty Slot", -1, 0, 0, Game.Items.GameItems.TypeOfItem.Seed);
                            }));
                        }
                    }
                    await Task.Delay(1000);
                    Dispatcher.Invoke(new Action(() =>
                    {
                        UpdateGarden();
                    }));
                }
            });
        }

        private void btnPlant_Click(object sender, RoutedEventArgs e)
        {
            var selectedSlot = dbGarden.SelectedIndex;
            var selectedSeed = (Game.Items.GameItems)cbSeeds.SelectedItem;

            if(selectedSlot > -1 && cbSeeds.SelectedIndex > -1)
            {
                gameSession.Hero.PlayersGarden.Slots[selectedSlot] = selectedSeed;
                gameSession.Hero.PlayersGarden.AvailableSeeds.Remove(selectedSeed);
                Growing();
                UpdateGarden();
            }
        }
    }
}
