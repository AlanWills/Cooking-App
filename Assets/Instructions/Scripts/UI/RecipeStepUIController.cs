using Celeste.Events;
using Cooking.Core.Parameters;
using Cooking.Core.Runtime;
using System;
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
        [SerializeField] private TMP_InputField stepDescription;
        [SerializeField] private Image stepImage;
        [SerializeField] private TextMeshProUGUI stepUtilityText;
        [SerializeField] private TextMeshProUGUI editButtonText;
        [SerializeField] private Button tipButton;
        [SerializeField] private Button warningButton;
        [SerializeField] private Button recommendationButton;
        [SerializeField] private Button explanationButton;
        [SerializeField] private Button previousStepButton;
        [SerializeField] private TextMeshProUGUI previousStepButtonText;
        [SerializeField] private TextMeshProUGUI nextStepButtonText;

        [Header("Data")]
        [SerializeField] private RecipeRuntimeValue currentRecipe;

        [NonSerialized] private RecipeStepRuntime currentRecipeStep;

        #endregion

        private void RefreshUI(int currentStepIndex)
        {
            if (currentStepIndex >= currentRecipe.Value.NumSteps)
            {
                // Don't refresh the UI if we've moved past the final step
                Debug.LogAssertion($"Reached invalid step index {currentRecipeStep} of recipe {currentRecipe.Value.DisplayName}.  Was expecting a number less than {currentRecipe.Value.NumSteps}.");
                return;
            }

            currentRecipeStep = currentRecipe.Value.GetStep(currentStepIndex);
            stepTitle.text = currentRecipeStep.Title;
            stepDescription.text = currentRecipeStep.Description;
            stepImage.sprite = currentRecipeStep.HasImages ? currentRecipeStep.Images[0] : null;

            stepUtilityText.text = "";
            tipButton.interactable = !string.IsNullOrEmpty(currentRecipeStep.Tip);
            warningButton.interactable = !string.IsNullOrEmpty(currentRecipeStep.Warning);
            recommendationButton.interactable = !string.IsNullOrEmpty(currentRecipeStep.Recommendation);
            explanationButton.interactable = !string.IsNullOrEmpty(currentRecipeStep.Explanation);

            previousStepButton.interactable = currentStepIndex > 0;
            previousStepButtonText.color = currentStepIndex > 0 ? Color.white : Color.black;
            nextStepButtonText.text = currentStepIndex < currentRecipe.Value.NumSteps - 1 ? "Next" : "Finish";
        }

        #region Callbacks

        public void OnCurrentStepChanged(ValueChangedArgs<int> args)
        {
            RefreshUI(args.newValue);
        }

        public void OnEditPressed()
        {
            if (stepDescription.IsInteractable())
            {
                currentRecipeStep.Description = stepDescription.text;
                stepDescription.DeactivateInputField();
                stepDescription.interactable = false;
                editButtonText.text = "Edit";
            }
            else
            {
                stepDescription.interactable = true;
                stepDescription.ActivateInputField();
                editButtonText.text = "Save";
            }
        }

        public void OnTipPressed()
        {
            stepUtilityText.text = currentRecipeStep.Tip;
        }

        public void OnWarningPressed()
        {
            stepUtilityText.text = currentRecipeStep.Warning;
        }

        public void OnRecommendationPressed()
        {
            stepUtilityText.text = currentRecipeStep.Recommendation;
        }

        public void OnExplanationPressed()
        {
            stepUtilityText.text = currentRecipeStep.Explanation;
        }

        #endregion
    }
}
