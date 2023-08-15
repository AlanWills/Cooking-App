using Celeste.Events;
using Cooking.Core.Parameters;
using System;
using UnityEngine;

namespace Cooking.Instructions.UI
{
    [AddComponentMenu("Cooking/UI/Recipe Step UI Controller")]
    public class RecipeStepUIController : MonoBehaviour
    {
        #region Properties and Fields

        [Header("UI Elements")]
        [SerializeField] private RecipeStepNonEditModeUI nonEditModeUI;
        [SerializeField] private RecipeStepEditModeUI editModeUI;

        [Header("Data")]
        [SerializeField] private RecipeRuntimeValue currentRecipe;

        [Header("Events")]
        [SerializeField] private ShowPopupEvent showWebCamCapturePopupEvent;

        [NonSerialized] private int currentStepIndex;

        #endregion

        private void RefreshUI(int currentStepIndex)
        {
            this.currentStepIndex = currentStepIndex;

            StopEditing();
        }

        private void StartEditing()
        {
            nonEditModeUI.TearDownUI();
            nonEditModeUI.gameObject.SetActive(false);
            editModeUI.gameObject.SetActive(true);
            editModeUI.SetupUI(currentStepIndex);
        }

        private void StopEditing()
        {
            editModeUI.TearDownUI();
            editModeUI.gameObject.SetActive(false);
            nonEditModeUI.gameObject.SetActive(true);
            nonEditModeUI.SetupUI(currentStepIndex);
        }

        #region Callbacks

        public void OnCurrentStepChanged(ValueChangedArgs<int> args)
        {
            RefreshUI(args.newValue);
        }

        public void OnEditPressed()
        {
            StartEditing();
        }

        public void OnSavePressed()
        {
            StopEditing();
        }

        public void OnRemoveStepPressed()
        {
            currentRecipe.Value.RemoveStep(currentStepIndex);

            // Show the same step index, unless this was the very last step, in which case show the new last step
            RefreshUI(Mathf.Min(currentStepIndex, currentRecipe.Value.NumSteps - 1));
        }

        public void OnAddStepPressed()
        {
            currentRecipe.Value.InsertStep(currentStepIndex);

            RefreshUI(currentStepIndex + 1);
        }

        #endregion
    }
}
