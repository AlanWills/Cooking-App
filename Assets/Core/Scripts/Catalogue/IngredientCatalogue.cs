using UnityEngine;
using Celeste.Objects;
using Cooking.Core.Objects;

namespace Cooking.Core.Catalogue
{
    [CreateAssetMenu(fileName = nameof(IngredientCatalogue), menuName = "Cooking/Core/Ingredient Catalogue")]
    public class IngredientCatalogue : ListScriptableObject<Ingredient>
    {
        public Ingredient FindByGuid(string guid)
        {
            return FindItem(x => string.CompareOrdinal(guid, x.Guid) == 0);
        }
    }
}