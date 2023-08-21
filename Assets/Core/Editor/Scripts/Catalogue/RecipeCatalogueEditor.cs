using UnityEditor;
using CelesteEditor.DataStructures;
using Cooking.Core.Objects;
using Cooking.Core.Catalogue;
using UnityEngine;
using Cooking.Core.Record;
using static UnityEditor.EditorGUI;
using CelesteEditor;

namespace CookingEditor.Core.Catalogue
{
    [CustomEditor(typeof(RecipeCatalogue))]
    public class RecipeCatalogueEditor : IIndexableItemsEditor<Recipe>
    {
        private ImageCatalogue imageCatalogue;

        public override void OnInspectorGUI()
        {
            imageCatalogue = CelesteEditorGUILayout.ObjectField(imageCatalogue);

            using (new DisabledScope(imageCatalogue == null))
            {
                if (GUILayout.Button("Bake Recipe Sprites"))
                {
                    (target as RecipeCatalogue).AddRecipeImagesTo(imageCatalogue);
                    AssetDatabase.SaveAssetIfDirty(target);
                }
            }

            base.OnInspectorGUI();
        }
    }
}