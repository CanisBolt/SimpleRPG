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

        // That's one big bad code. Works for now, but want to rewrite it later
        private void btnCraft_Click(object sender, RoutedEventArgs e)
        {
            Game.Items.AlchemyRecipe selectedRecipe = (Game.Items.AlchemyRecipe)cbRecipeList.SelectedItem;
            List<bool> isAllItemsAvailable = new List<bool>(); // list for checking purpose

            foreach (KeyValuePair<Game.Items.GameItems, int> recipe in selectedRecipe.RequiredToCraft)
            {
                foreach(var item in gameSession.Hero.Inventory)
                {
                    if(item.ID.Equals(recipe.Key.ID))
                    {
                        if(item.Quantity >= recipe.Value)
                        {
                            isAllItemsAvailable.Add(true); // true if ingredient is available
                        }
                    }
                }
            }

            // moving through list once again to remove items that were used in crafting
            if (isAllItemsAvailable.Count.Equals(selectedRecipe.RequiredToCraft.Count))
            {
                foreach (KeyValuePair<Game.Items.GameItems, int> recipe in selectedRecipe.RequiredToCraft)
                {
                    for(int i = 0; i < gameSession.Hero.Inventory.Count; i++)
                    {
                        if (gameSession.Hero.Inventory[i].ID.Equals(recipe.Key.ID))
                        {
                            if (gameSession.Hero.Inventory[i].Quantity >= recipe.Value)
                            {
                                gameSession.Hero.RemoveItemToInventory(gameSession.Hero.Inventory[i], recipe.Value);
                                i = 0;
                            }
                        }
                    }
                }
                gameSession.Hero.AddItemToInventory(Game.Items.ItemsFactory.ItemByID(selectedRecipe.CraftingItem.ID));
            }
            else
            {
                MessageBox.Show($"You don't have ingredients to craft {selectedRecipe.RecipeName}");
                return;
            }
        }
    }
}
