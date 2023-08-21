using CelesteEditor.BuildSystem.Steps;
using Cooking.Core.Catalogue;
using UnityEditor;
using UnityEngine;

namespace CookingEditor.Core.BuildSystem
{
    [CreateAssetMenu(fileName = "BakeRecipeSpritesAssetPreparationStep", menuName = "Cooking/Asset Preparation/Bake Recipe Sprites")]
    public class BakeRecipeSpritesIntoCatalogueAssetPreparationStep : AssetPreparationStep
    {
        #region Properties and Fields

        [SerializeField] private RecipeCatalogue recipeCatalogue;
        [SerializeField] private ImageCatalogue imageCatalogue;

        #endregion

        public override void Execute()
        {
            recipeCatalogue.AddRecipeImagesTo(imageCatalogue);
            AssetDatabase.SaveAssetIfDirty(recipeCatalogue);
        }
    }
}
