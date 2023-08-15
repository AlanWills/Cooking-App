using Cooking.Core.Record;
using Cooking.Core.Runtime;
using System;
using System.Collections.Generic;

namespace Cooking.Core.Persistence
{
    [Serializable]
    public class RecipeManagerDTO
    {
        public List<RecipeDTO> recipeDTOs = new List<RecipeDTO>();

        public RecipeManagerDTO(RecipeRecord recipeRecord)
        {
            recipeDTOs.Capacity = recipeRecord.NumRecipes;

            for (int i = 0, n = recipeRecord.NumRecipes; i < n; ++i)
            {
                RecipeRuntime recipe = recipeRecord.GetRecipe(i);

                if (recipe.HasCustomEdits)
                {
                    recipeDTOs.Add(new RecipeDTO(recipe));
                }
            }
        }
    }
}