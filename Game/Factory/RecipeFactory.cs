using Game.Items;
using System.Collections.Generic;

namespace Game.Factory
{
    public class RecipeFactory
    {
        private static List<AlchemyRecipe> recipeList;

        static RecipeFactory()
        {
            recipeList = new List<AlchemyRecipe>();

            AlchemyRecipe smallHealingPotion = new AlchemyRecipe("Small Healing Potion", World.AlchemyRecipeIDSmallHealingPotion, ItemsFactory.CreateGameItem(World.ItemIDSmallHealingPotion));
            smallHealingPotion.AddIngredient(World.MaterialIDHealingGrass, 2);

            AlchemyRecipe smallManaPotion = new AlchemyRecipe("Small Mana Potion", World.AlchemyRecipeIDSmallManaPotion, ItemsFactory.CreateGameItem(World.ItemIDSmallManaPotion));
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
