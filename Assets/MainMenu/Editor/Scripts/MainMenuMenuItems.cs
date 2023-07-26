using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace MainMenuEditor
{
   public static class MenuItems
   {
       [MenuItem(MainMenuEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadMainMenuMenuItem()
       {
           LoadSceneSetMenuItem(MainMenuEditorConstants.SCENE_SET_PATH);
       }
   }
}