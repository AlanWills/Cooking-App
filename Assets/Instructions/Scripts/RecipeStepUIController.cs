﻿using Celeste.Events;
using Cooking.Core.Parameters;
using Cooking.Core.Runtime;
using Cooking.Core.UI;
using System;
using System.Collections.Generic;
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
        [SerializeField] private ImageCarouselView stepImages;
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

        [Header("Events")]
        [SerializeField] private ShowPopupEvent showWebCamCapturePopupEvent;

        [NonSerialized] private RecipeStepRuntime currentRecipeStep;

        #endregion

        private void SetupUI(int currentStepIndex)
        {
            if (currentStepIndex >= currentRecipe.Value.NumSteps)
            {
                // Don't refresh the UI if we've moved past the final step
                Debug.LogAssertion($"Reached invalid step index {currentRecipeStep} of recipe {currentRecipe.Value.DisplayName}.  Was expecting a number less than {currentRecipe.Value.NumSteps}.");
                return;
            }

            currentRecipeStep = currentRecipe.Value.GetStep(currentStepIndex);

            previousStepButton.interactable = currentStepIndex > 0;
            previousStepButtonText.color = currentStepIndex > 0 ? Color.white : Color.black;
            nextStepButtonText.text = currentStepIndex < currentRecipe.Value.NumSteps - 1 ? "Next" : "Finish";

            RefreshCurrentStepUI();
        }

        private void RefreshCurrentStepUI()
        {
            stepTitle.text = currentRecipeStep.Title;
            stepDescription.text = currentRecipeStep.Description;

            RefreshImages();

            stepUtilityText.text = "";
            tipButton.interactable = !string.IsNullOrEmpty(currentRecipeStep.Tip);
            warningButton.interactable = !string.IsNullOrEmpty(currentRecipeStep.Warning);
            recommendationButton.interactable = !string.IsNullOrEmpty(currentRecipeStep.Recommendation);
            explanationButton.interactable = !string.IsNullOrEmpty(currentRecipeStep.Explanation);
        }

        private void RefreshImages()
        {
            List<ImageCarouselData> stepImagesData = new List<ImageCarouselData>();
            if (currentRecipeStep.HasImages)
            {
                foreach (Sprite image in currentRecipeStep.Images)
                {
                    stepImagesData.Add(new ImageCarouselData(image));
                }

                stepImages.Setup(stepImagesData);
            }
        }

        #region Callbacks

        public void OnCurrentStepChanged(ValueChangedArgs<int> args)
        {
            SetupUI(args.newValue);
        }

        public void OnTakePictureButtonPressed()
        {
            showWebCamCapturePopupEvent.Invoke(new WebCamCapturePopupArgs(currentRecipeStep));
        }

        public void OnWebCamCapturePopupClosed()
        {
            RefreshImages();
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
