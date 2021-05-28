using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Items
{
    public class RecipeFactory
    {
        private static List<AlchemyRecipe> recipeList;

        static RecipeFactory()
        {
            recipeList = new List<AlchemyRecipe>();

            AlchemyRecipe smallHealingPotion = new AlchemyRecipe("Small Healing Potion", World.AlchemyRecipeIDSmallHealingPotion, ItemsFactory.ItemByID(World.ItemIDSmallHealingPotion));
            smallHealingPotion.AddIngredient(World.MaterialIDHealingGrass, 2);


            recipeList.Add(smallHealingPotion);
        }

        public static AlchemyRecipe RecipeByID(int id)
        {
            foreach (AlchemyRecipe recipe in recipeList)
            {
                if (recipe.RecipeID.Equals(id))
                {
                    return recipe;
                }
            }
            return null;
        }
    }
}
