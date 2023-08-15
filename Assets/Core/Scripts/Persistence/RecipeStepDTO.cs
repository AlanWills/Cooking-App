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
            foreach (var edit in recipeStepRuntime.InitialEdits)
            {
                // Set up initial state
                edits.Add(new RecipeStepEditCommandDTO(edit));
            }

            foreach (var edit in recipeStepRuntime.CustomEdits)
            {
                // Apply custom edits afterwards, allowing us to fully resurrect this object
                edits.Add(new RecipeStepEditCommandDTO(edit));
            }
        }
    }
}
