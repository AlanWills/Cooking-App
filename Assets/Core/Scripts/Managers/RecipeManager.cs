using UnityEngine;
using Celeste.Persistence;
using Cooking.Core.Persistence;
using Cooking.Core.Catalogue;
using Cooking.Core.Record;
using Cooking.Core.Runtime;
using Cooking.Core.Objects;
using System.Collections.Generic;

namespace Cooking.Core.Managers
{
    [AddComponentMenu("Cooking/Core/Recipe Manager")]
    public class RecipeManager : PersistentSceneManager<RecipeManager, RecipeManagerDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "RecipeManager.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private RecipeCatalogue recipeCatalogue;
        [SerializeField] private RecipeRecord recipeRecord;
        [SerializeField] private IngredientCatalogue ingredientCatalogue;

        #endregion

        #region Save/Load

        protected override RecipeManagerDTO Serialize()
        {
            return new RecipeManagerDTO(recipeRecord);
        }

        protected override void Deserialize(RecipeManagerDTO dto)
        {
            foreach (RecipeDTO recipeDTO in dto.recipeDTOs)
            {
                RecipeRuntime recipeRuntime = new RecipeRuntime(recipeDTO.guid, recipeDTO.displayName);
                recipeRuntime.Load(recipeDTO, ingredientCatalogue);
                recipeRuntime.AddOnRecipeChangedCallback(OnRecipeChanged);
                recipeRecord.AddRecipe(recipeRuntime);
            }

            foreach (Recipe recipe in recipeCatalogue)
            {
                if (recipeRecord.FindRecipe(x => x.Guid == recipe.Guid) == null)
                {
                    RecipeRuntime recipeRuntime = new RecipeRuntime(recipe);
                    recipeRuntime.AddOnRecipeChangedCallback(OnRecipeChanged);
                    recipeRecord.AddRecipe(recipeRuntime);
                }
            }
        }

        protected override void SetDefaultValues()
        {
            foreach (Recipe recipe in recipeCatalogue)
            {
                RecipeRuntime recipeRuntime = new RecipeRuntime(recipe);
                recipeRuntime.AddOnRecipeChangedCallback(OnRecipeChanged);
                recipeRecord.AddRecipe(recipeRuntime);
            }
        }

        #endregion

        #region Callbacks

        private void OnRecipeChanged()
        {
            Save();
        }

        #endregion
    }
}