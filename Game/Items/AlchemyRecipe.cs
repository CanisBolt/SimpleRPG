using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Items
{
    public class AlchemyRecipe
    {
        public string RecipeName { get; set; }
        public int RecipeID { get; set; }
        public GameItems CraftingItem { get; set; }
        public Dictionary<GameItems, int> RequiredToCraft { get; set; } 

        public AlchemyRecipe(string recipeName, int recipeID, GameItems craftingItem)
        {
            RecipeName = recipeName;
            RecipeID = recipeID;
            CraftingItem = craftingItem;
            RequiredToCraft = new Dictionary<GameItems, int>();
        }

        public void AddIngredient(int itemID, int quantity)
        {
            RequiredToCraft.Add(ItemsFactory.ItemByID(itemID), quantity);
        }
    }
}
