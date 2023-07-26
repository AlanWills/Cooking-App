using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace CookingEditor.Instructions
{
   public static class MenuItems
   {
       [MenuItem(CookingInstructionsEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadCookingInstructionsMenuItem()
       {
           LoadSceneSetMenuItem(CookingInstructionsEditorConstants.SCENE_SET_PATH);
       }
   }
}