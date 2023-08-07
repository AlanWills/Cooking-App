using Cooking.Core.Runtime;
using System;

namespace Cooking.Core.Persistence
{
    [Serializable]
    public class RecipeStepDTO
    {
        public string description;

        public RecipeStepDTO(RecipeStepRuntime recipeStepRuntime)
        {
            description = recipeStepRuntime.Description;
        }
    }
}
