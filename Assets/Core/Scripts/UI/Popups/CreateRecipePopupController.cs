using Celeste.Events;
using Celeste.UI;
using Cooking.Core.Record;
using Cooking.Core.Runtime;
using TMPro;
using UnityEngine;

namespace Cooking.Core.UI
{
    [AddComponentMenu("Cooking/Core/UI/Create Recipe Popup Controller")]
    public class CreateRecipePopupController : MonoBehaviour, IPopupController
    {
        #region Properties and Fields

        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private RecipeRecord recipeRecord;

        #endregion

        #region IPopupController

        public void OnShow(IPopupArgs args) { }

        public void OnHide() { }

        public void OnConfirmPressed()
        {
            RecipeRuntime newRecipe = new RecipeRuntime(nameInput.text);
            newRecipe.AddStep();
            recipeRecord.AddRecipe(newRecipe);
        }

        public void OnClosePressed() { }

        #endregion
    }
}
