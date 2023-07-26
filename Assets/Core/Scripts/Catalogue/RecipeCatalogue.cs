using UnityEngine;
using Celeste.Objects;
using Cooking.Core.Objects;

namespace Cooking.Core.Catalogue
{
    [CreateAssetMenu(fileName = nameof(RecipeCatalogue), menuName = "Cooking/Core/Recipe Catalogue")]
    public class RecipeCatalogue : ListScriptableObject<Recipe>
    {
    }
}