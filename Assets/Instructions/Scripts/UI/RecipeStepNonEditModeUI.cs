﻿using Cooking.Core.Parameters;
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
    [AddComponentMenu("Cooking/Instructions/UI/Recipe Step Non Edit Mode UI")]
    public class RecipeStepNonEditModeUI : MonoBehaviour
    {
        #region Properties and Fields

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI stepTitle;
        [SerializeField] private TextMeshProUGUI stepDescription;
        [SerializeField] private ImageCarouselView stepImages;
        [SerializeField] private TextMeshProUGUI stepUtilityText;
        [SerializeField] private Button tipButton;
        [SerializeField] private Button warningButton;
        [SerializeField] private Button recommendationButton;
        [SerializeField] private Button explanationButton;
        [SerializeField] private Button previousStepButton;
        [SerializeField] private TextMeshProUGUI previousStepButtonText;
        [SerializeField] private TextMeshProUGUI nextStepButtonText;

        [Header("Data")]
        [SerializeField] private ImageRecord imageRecord;
        [SerializeField] private RecipeRuntimeValue currentRecipe;

        [NonSerialized] private RecipeStepRuntime currentRecipeStep;

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

            previousStepButton.interactable = currentStepIndex > 0;
            previousStepButtonText.color = currentStepIndex > 0 ? Color.white : Color.black;
            nextStepButtonText.text = currentStepIndex < currentRecipe.Value.NumSteps - 1 ? "Next" : "Finish";

            RefreshCurrentStepUI();
        }

        public void TearDownUI()
        {
            currentRecipeStep = null;
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

        #region Callbacks

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
