using UnityEditor;
using CelesteEditor.DataStructures;
using Cooking.Core.Objects;
using Cooking.Core.Catalogue;

namespace CookingEditor.Core.Catalogue
{
    [CustomEditor(typeof(RecipeCatalogue))]
    public class RecipeCatalogueEditor : IIndexableItemsEditor<Recipe>
    {
    }
}