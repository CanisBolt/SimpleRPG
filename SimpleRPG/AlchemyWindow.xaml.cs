using Game;
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

namespace SimpleRPG
{
    /// <summary>
    /// Логика взаимодействия для AlchemyWindow.xaml
    /// </summary>
    public partial class AlchemyWindow : Window
    {
        GameSession gameSession;
        public AlchemyWindow(GameSession _gameSession)
        {
            InitializeComponent();

            gameSession = _gameSession;

            foreach(var recipe in gameSession.Hero.RecipeList)
            {
                cbRecipeList.Items.Add(recipe);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Game.Items.AlchemyRecipe selectedRecipe = (Game.Items.AlchemyRecipe)cbRecipeList.SelectedItem;
            tbRequired.Text += $"To craft {selectedRecipe.RecipeName}:" + Environment.NewLine;

            foreach (KeyValuePair<Game.Items.GameItems, int> recipe in selectedRecipe.RequiredToCraft)
            {
                tbRequired.Text += $"Item: {recipe.Key.Name} Amount: {recipe.Value}" + Environment.NewLine;
            }
        }

        // For now it's possible to craft item only from one igredient.
        private void btnCraft_Click(object sender, RoutedEventArgs e)
        {
            Game.Items.AlchemyRecipe selectedRecipe = (Game.Items.AlchemyRecipe)cbRecipeList.SelectedItem;

            foreach (KeyValuePair<Game.Items.GameItems, int> recipe in selectedRecipe.RequiredToCraft)
            {
                foreach(var item in gameSession.Hero.Inventory)
                {
                    if(item.ID.Equals(recipe.Key.ID))
                    {
                        if(item.Quantity >= recipe.Value)
                        {
                            gameSession.Hero.AddItemToInventory(Game.Items.ItemsFactory.ItemByID(selectedRecipe.CraftingItem.ID));
                            gameSession.Hero.RemoveItemToInventory(item, recipe.Value);
                            return;
                        }
                    }
                }
                MessageBox.Show("You don't have items to craft");
                return;
            }
        }
    }
}
