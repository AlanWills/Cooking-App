using Cooking.Core.Runtime;
using UnityEngine;

namespace Cooking.Core.UI
{
    public class ImageCarouselData
    {
        public Sprite Image { get; }

        public ImageCarouselData(ImageRuntime imageRuntime)
        {
            Image = imageRuntime.Image;
        }
    }
}
