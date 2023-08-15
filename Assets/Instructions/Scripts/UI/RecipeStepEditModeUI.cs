using Celeste.Events;
using Cooking.Core.Commands;
using Cooking.Core.Parameters;
using Cooking.Core.Record;
using Cooking.Core.Runtime;
using Cooking.Core.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking.Instructions.UI
{
    [AddComponentMenu("Cooking/Instructions/UI/Recipe Step Edit Mode UI")]
    public class RecipeStepEditModeUI : MonoBehaviour
    {
        #region Enums

        private enum UtilityTextEditMode
        {
            None,
            Tip,
            Warning,
            Recommendation,
            Explanation
        }

        #endregion

        #region Properties and Fields

        [SerializeField] private TMP_InputField stepTitle;
        [SerializeField] private TMP_InputField stepDescription;
        [SerializeField] private ImageCarouselView stepImages;
        [SerializeField] private GameObject stepTipTextContainer;
        [SerializeField] private GameObject stepWarningTextContainer;
        [SerializeField] private GameObject stepRecommendationTextContainer;
        [SerializeField] private GameObject stepExplanationTextContainer;
        [SerializeField] private TMP_InputField stepTipText;
        [SerializeField] private TMP_InputField stepWarningText;
        [SerializeField] private TMP_InputField stepRecommendationText;
        [SerializeField] private TMP_InputField stepExplanationText;
        [SerializeField] private Button removeStepButton;
        [SerializeField] private TextMeshProUGUI removeStepButtonText;

        [Header("Data")]
        [SerializeField] private RecipeRuntimeValue currentRecipe;
        [SerializeField] private ImageRecord imageRecord;

        [Header("Events")]
        [SerializeField] private ShowPopupEvent showWebCamCapturePopupEvent;

        [NonSerialized] private RecipeStepRuntime currentRecipeStep;
        [NonSerialized] private UtilityTextEditMode utilityTextEditMode;

        #endregion

        public void SetupUI(int currentStepIndex)
        {
            if (currentStepIndex >= currentRecipe.Value.NumSteps)
            {
                // Don't refresh the UI if we've moved past the final step
                Debug.LogAssertion($"Reached invalid step index {currentRecipeStep} of recipe {currentRecipe.Value.DisplayName}.  Was expecting a number less than {currentRecipe.Value.NumSteps}.");
                return;
            }

            currentRecipeStep = currentRecipe.Value.GetStep(currentStepIndex);

            removeStepButton.interactable = currentStepIndex > 0;
            removeStepButtonText.color = currentStepIndex > 0 ? Color.white : Color.black;

            RefreshCurrentStepUI();
        }

        public void TearDownUI()
        {
            currentRecipeStep = null;
            utilityTextEditMode = UtilityTextEditMode.None;
            stepTitle.text = string.Empty;
            stepDescription.text = string.Empty;
            stepTipText.text = string.Empty;
            stepWarningText.text = string.Empty;
            stepRecommendationText.text = string.Empty;
            stepExplanationText.text = string.Empty;
        }

        private void RefreshCurrentStepUI()
        {
            stepTitle.text = currentRecipeStep.Title;
            stepDescription.text = currentRecipeStep.Description;
            stepTipText.text = currentRecipeStep.Tip;
            stepWarningText.text = currentRecipeStep.Warning;
            stepRecommendationText.text = currentRecipeStep.Recommendation;
            stepExplanationText.text = currentRecipeStep.Explanation;

            RefreshImages();
            RefreshUtilityText(UtilityTextEditMode.None);
        }

        private void RefreshImages()
        {
            List<ImageCarouselData> stepImagesData = new List<ImageCarouselData>();
            if (currentRecipeStep.HasImages)
            {
                foreach (ImageRuntime image in currentRecipeStep.Images)
                {
                    if (image.NeedsResolving)
                    {
                        image.Resolve(imageRecord);
                    }

                    stepImagesData.Add(new ImageCarouselData(image));
                }
            }

            stepImages.Setup(stepImagesData);
        }

        private void RefreshUtilityText(UtilityTextEditMode newEditMode)
        {
            if (utilityTextEditMode == newEditMode &&
                utilityTextEditMode != UtilityTextEditMode.None)
            {
                // The current edit modes are equal and not None
                return;
            }

            // We treat the None case as a 'force' because we want to make sure our UI is always set up correctly when it's turned off with None
            utilityTextEditMode = newEditMode;
            stepTipTextContainer.gameObject.SetActive(newEditMode == UtilityTextEditMode.Tip);
            stepWarningTextContainer.gameObject.SetActive(newEditMode == UtilityTextEditMode.Warning);
            stepRecommendationTextContainer.gameObject.SetActive(newEditMode == UtilityTextEditMode.Recommendation);
            stepExplanationTextContainer.gameObject.SetActive(newEditMode == UtilityTextEditMode.Explanation);
        }

        #region Callbacks

        public void OnTakePictureButtonPressed()
        {
            showWebCamCapturePopupEvent.Invoke(new WebCamCapturePopupArgs(currentRecipeStep));
        }

        public void OnWebCamCapturePopupClosed()
        {
            RefreshImages();
        }

        public void OnSavePressed()
        {
            currentRecipeStep.Title = stepTitle.text;
            currentRecipeStep.Description = stepDescription.text;
            currentRecipeStep.Tip = stepTipText.text;
            currentRecipeStep.Warning = stepWarningText.text;
            currentRecipeStep.Recommendation = stepRecommendationText.text;
            currentRecipeStep.Explanation = stepExplanationText.text;
        }

        public void OnDiscardPressed()
        {
            // How will we remove the images we've added?
            // Maybe we need to change the web cam capture so that it tells us what images were added somehow
            // Or have an on the recipe step called 'OnEditApplied' that we can keep track of all the changes with and revert them if needs be
        }

        public void OnTipPressed()
        {
            RefreshUtilityText(UtilityTextEditMode.Tip);
        }

        public void OnWarningPressed()
        {
            RefreshUtilityText(UtilityTextEditMode.Warning);
        }

        public void OnRecommendationPressed()
        {
            RefreshUtilityText(UtilityTextEditMode.Recommendation);
        }

        public void OnExplanationPressed()
        {
            RefreshUtilityText(UtilityTextEditMode.Explanation);
        }

        #endregion
    }
}
