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

            AlchemyRecipe smallManaPotion = new AlchemyRecipe("Small Mana Potion", World.AlchemyRecipeIDSmallManaPotion, ItemsFactory.ItemByID(World.ItemIDSmallManaPotion));
            smallManaPotion.AddIngredient(World.EnemyLootIDRatTail, 2);
            smallManaPotion.AddIngredient(World.EnemyLootIDSnakeFang, 1);

            recipeList.Add(smallHealingPotion);
            recipeList.Add(smallManaPotion);
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
