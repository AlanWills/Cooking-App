using Celeste.Events;
using Cooking.Core.Objects;
using Cooking.Core.Parameters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking.Instructions.UI
{
    [AddComponentMenu("Cooking/UI/Recipe Step UI Controller")]
    public class RecipeStepUIController : MonoBehaviour
    {
        #region Properties and Fields

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI stepTitle;
        [SerializeField] private TextMeshProUGUI stepDescription;
        [SerializeField] private Image stepImage;
        [SerializeField] private Button previousStepButton;
        [SerializeField] private TextMeshProUGUI previousStepButtonText;
        [SerializeField] private Button nextStepButton;
        [SerializeField] private TextMeshProUGUI nextStepButtonText;

        [Header("Data")]
        [SerializeField] private RecipeRuntimeValue currentRecipe;

        #endregion

        private void RefreshUI(int currentStepIndex)
        {
            RecipeStep step = currentRecipe.Value.GetStep(currentStepIndex);
            stepTitle.text = step.Title;
            stepDescription.text = step.Description;
            stepImage.sprite = step.HasImages ? step.Images[0] : null;
            previousStepButton.interactable = currentStepIndex > 0;
            previousStepButtonText.color = currentStepIndex > 0 ? Color.white : Color.black;
            nextStepButton.interactable = currentStepIndex < currentRecipe.Value.NumSteps - 1;
            nextStepButtonText.color = currentStepIndex < currentRecipe.Value.NumSteps - 1 ? Color.white : Color.black;
        }

        #region Callbacks

        public void OnCurrentStepChanged(ValueChangedArgs<int> args)
        {
            RefreshUI(args.newValue);
        }

        #endregion
    }
}
