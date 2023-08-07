using Cooking.Core.Record;
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
                recipeDTOs.Add(new RecipeDTO(recipeRecord.GetRecipe(i)));
            }
        }
    }
}