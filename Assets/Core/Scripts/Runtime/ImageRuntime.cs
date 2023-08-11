using Cooking.Core.Record;
using UnityEngine;

namespace Cooking.Core.Runtime
{
    public class ImageRuntime
    {
        #region Properties and Fields

        public bool NeedsResolving => Image == null;
        public Sprite Image { get; private set; }
        public string ImageId { get; }

        #endregion

        public ImageRuntime(Sprite image)
        {
            Image = image;
            ImageId = string.Empty;
        }

        public ImageRuntime(string imageId)
        {
            Image = null;
            ImageId = imageId;
        }

        public bool Resolve(ImageRecord imageRecord)
        {
            Image = imageRecord.GetItem(ImageId);
            return Image != null;
        }
    }
}
