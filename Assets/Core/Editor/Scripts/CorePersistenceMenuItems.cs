using UnityEditor;
using Celeste.Persistence;
using CelesteEditor.Persistence;
using Cooking.Core.Managers;

namespace CookingEditor.Core.Persistence
{
    public static class CorePersistenceMenuItems
    {
        [MenuItem("Cooking/Save/Open Recipe Manager Save", priority = 0)]
        public static void OpenCoreSaveMenuItem()
        {
            PersistenceMenuItemUtility.OpenExplorerAtPersistentData();
        }

        [MenuItem("Cooking/Save/Delete Recipe Manager Save", priority = 100)]
        public static void DeleteCoreSaveMenuItem()
        {
            PersistenceUtility.DeletePersistentDataFile(RecipeManager.FILE_NAME);
        }
    }
}
