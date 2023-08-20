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
        private ImageRecord imageRecord;

        public override void OnInspectorGUI()
        {
            imageRecord = CelesteEditorGUILayout.ObjectField(imageRecord);

            using (new DisabledScope(imageRecord == null))
            {
                if (GUILayout.Button("Bake Recipe Sprites"))
                {
                    (target as RecipeCatalogue).AddRecipeImagesTo(imageRecord);
                    AssetDatabase.SaveAssetIfDirty(target);
                }
            }

            base.OnInspectorGUI();
        }
    }
}