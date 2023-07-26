using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace CookingEditor.Bootstrap
{
   public static class MenuItems
   {
       [MenuItem(CookingBootstrapEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadCookingBootstrapMenuItem()
       {
           LoadSceneSetMenuItem(CookingBootstrapEditorConstants.SCENE_SET_PATH);
       }
   }
}