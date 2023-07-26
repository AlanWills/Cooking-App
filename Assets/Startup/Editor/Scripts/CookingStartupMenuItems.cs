using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace CookingEditor.Startup
{
   public static class MenuItems
   {
       [MenuItem(CookingStartupEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadCookingStartupMenuItem()
       {
           LoadSceneSetMenuItem(CookingStartupEditorConstants.SCENE_SET_PATH);
       }
   }
}