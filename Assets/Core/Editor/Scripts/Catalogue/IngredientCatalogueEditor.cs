using UnityEngine;
using UnityEditor;
using CelesteEditor.DataStructures;
using Cooking.Core.Objects;
using Cooking.Core.Catalogue;

namespace CookingEditor.Core.Catalogue
{
    [CustomEditor(typeof(IngredientCatalogue))]
    public class IngredientCatalogueEditor : IIndexableItemsEditor<Ingredient>
    {
    }
}