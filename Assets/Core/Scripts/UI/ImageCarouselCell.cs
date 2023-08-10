using FancyCarouselView.Runtime.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking.Core.UI
{
    public class ImageCarouselCell : CarouselCell<ImageCarouselData, ImageCarouselCell>
    {
        [SerializeField] private Image image;

        protected override void Refresh(ImageCarouselData itemData)
        {
            image.sprite = itemData.Image;
        }
    }
}
