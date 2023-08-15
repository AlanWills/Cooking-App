using Cooking.Core.Runtime;
using System;
using System.Collections.Generic;

namespace Cooking.Core.Persistence
{
    [Serializable]
    public class RecipeDTO
    {
        public string guid;
        public string displayName;
        public List<RecipeEditCommandDTO> edits = new List<RecipeEditCommandDTO>();
        public List<RecipeStepDTO> recipeStepDTOs = new List<RecipeStepDTO>();

        public RecipeDTO(RecipeRuntime recipeRuntime)
        {
            guid = recipeRuntime.Guid;
            displayName = recipeRuntime.DisplayName;
            recipeStepDTOs.Capacity = recipeRuntime.NumSteps;

            foreach (var edit in recipeRuntime.InitialEdits)
            {
                edits.Add(new RecipeEditCommandDTO(edit));
            }

            foreach (var edit in recipeRuntime.CustomEdits)
            {
                edits.Add(new RecipeEditCommandDTO(edit));
            }

            for (int i = 0, n = recipeRuntime.NumSteps; i < n; ++i)
            {
                recipeStepDTOs.Add(new RecipeStepDTO(recipeRuntime.GetStep(i)));
            }
        }
    }
}
