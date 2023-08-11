using Cooking.Core.Runtime;
using System;
using System.Collections.Generic;

namespace Cooking.Core.Persistence
{
    [Serializable]
    public class RecipeStepDTO
    {
        public List<RecipeStepEditCommandDTO> edits = new List<RecipeStepEditCommandDTO>();

        public RecipeStepDTO(RecipeStepRuntime recipeStepRuntime)
        {
            foreach (var edit in recipeStepRuntime.Edits)
            {
                edits.Add(new RecipeStepEditCommandDTO(edit));
            }
        }
    }
}
