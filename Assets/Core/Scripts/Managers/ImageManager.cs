using UnityEngine;
using Celeste.Persistence;
using Cooking.Core.Persistence;
using Cooking.Core.Record;
using Cooking.Core.Catalogue;

namespace Cooking.Core.Managers
{
    [AddComponentMenu("Cooking/Core/Image Manager")]
    public class ImageManager : PersistentSceneManager<ImageManager, ImageManagerDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "ImageManager.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private ImageCatalogue imageCatalogue;
        [SerializeField] private ImageRecord imageRecord;

        #endregion

        #region Save/Load

        protected override ImageManagerDTO Serialize()
        {
            return new ImageManagerDTO(imageRecord);
        }

        protected override void Deserialize(ImageManagerDTO dto)
        {
            foreach (string recipeId in dto.images)
            {
                imageRecord.LoadImage(recipeId);
            }
        }

        protected override void SetDefaultValues() { }

        #endregion
    }
}