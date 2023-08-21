using Celeste.Objects;
using UnityEngine;

namespace Cooking.Core.Catalogue
{
    [CreateAssetMenu(fileName = nameof(ImageCatalogue), menuName = "Cooking/Core/Image Catalogue")]
    public class ImageCatalogue : ListScriptableObject<Sprite>
    {
    }
}
