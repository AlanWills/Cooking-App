using UnityEngine;
using Celeste.Persistence;
using Cooking.Core.Persistence;
using Cooking.Core.Catalogue;
using Cooking.Core.Record;
using Cooking.Core.Runtime;
using Cooking.Core.Objects;

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

        #endregion

        #region Save/Load

        protected override RecipeManagerDTO Serialize()
        {
            return new RecipeManagerDTO();
        }

        protected override void Deserialize(RecipeManagerDTO dto)
        {
            // Add recipes from save and then add any remaining recipes from the catalogue
        }

        protected override void SetDefaultValues()
        {
            foreach (Recipe recipe in recipeCatalogue)
            {
                recipeRecord.AddRecipe(new RecipeRuntime(recipe));
            }
        }

        #endregion
    }
}