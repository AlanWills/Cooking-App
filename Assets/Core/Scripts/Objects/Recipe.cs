using Celeste.Objects;
using UnityEngine;

namespace Cooking.Core.Objects
{
    [CreateAssetMenu(fileName = nameof(Recipe), menuName = "Cooking/Core/Recipe")]
    public class Recipe : ListScriptableObject<RecipeStep>
    {
        #region Properties and Fields

        public string DisplayName => displayName;
        public Sprite Thumbnail => thumbnail;

        [SerializeField] private string displayName;
        [SerializeField] private Sprite thumbnail;

        #endregion
    }
}