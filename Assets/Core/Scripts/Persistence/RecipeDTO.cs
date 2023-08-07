using Cooking.Core.Runtime;
using System;
using System.Collections.Generic;

namespace Cooking.Core.Persistence
{
    [Serializable]
    public class RecipeDTO
    {
        public string guid;
        public List<RecipeStepDTO> recipeStepDTOs = new List<RecipeStepDTO>();

        public RecipeDTO(RecipeRuntime recipeRuntime)
        {
            guid = recipeRuntime.Guid;
            recipeStepDTOs.Capacity = recipeRuntime.NumSteps;

            for (int i = 0, n = recipeRuntime.NumSteps; i < n; ++i)
            {
                recipeStepDTOs.Add(new RecipeStepDTO(recipeRuntime.GetStep(i)));
            }
        }
    }
}
