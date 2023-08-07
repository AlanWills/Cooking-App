using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;

namespace CookingEditor.Twine
{
    public static class MenuItems
    {
        [MenuItem("Cooking/Scenes/Load Twine")]
        public static void LoadTwineSceneSet()
        {
            LoadSceneSetMenuItem(CookingTwineEditorConstants.TWINE_SCENE_SET_PATH);
        }
    }
}