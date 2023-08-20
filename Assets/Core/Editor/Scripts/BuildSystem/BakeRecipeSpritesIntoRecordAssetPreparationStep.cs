using CelesteEditor.BuildSystem.Steps;
using Cooking.Core.Catalogue;
using Cooking.Core.Record;
using UnityEditor;
using UnityEngine;

namespace CookingEditor.Core.BuildSystem
{
    [CreateAssetMenu(fileName = "BakeRecipeSpritesAssetPreparationStep", menuName = "Cooking/Asset Preparation/Bake Recipe Sprites")]
    public class BakeRecipeSpritesIntoRecordAssetPreparationStep : AssetPreparationStep
    {
        #region Properties and Fields

        [SerializeField] private RecipeCatalogue recipeCatalogue;
        [SerializeField] private ImageRecord imageRecord;

        #endregion

        public override void Execute()
        {
            recipeCatalogue.AddRecipeImagesTo(imageRecord);
            AssetDatabase.SaveAssetIfDirty(recipeCatalogue);
        }
    }
}
