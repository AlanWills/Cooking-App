using Celeste.DataStructures;
using Cooking.Core.Objects;
using Cooking.Core.Persistence;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Core.Runtime
{
    public class RecipeRuntime
    {
        #region Properties and Fields

        public string Guid { get; }
        public string DisplayName { get; }
        public Sprite Thumbnail { get; }
        public int NumSteps => steps.Count;

        private List<RecipeStepRuntime> steps = new List<RecipeStepRuntime>();
        private Action onRecipeChangedEvent;

        #endregion

        public RecipeRuntime(Recipe recipe)
        {
            Guid = recipe.Guid;
            DisplayName = recipe.DisplayName;
            Thumbnail = recipe.Thumbnail;

            foreach (RecipeStep recipeStep in recipe.Steps)
            {
                RecipeStepRuntime recipeStepRuntime = new RecipeStepRuntime(recipeStep);
                recipeStepRuntime.AddOnRecipeStepChangedCallback(OnRecipeStepChanged);
                steps.Add(recipeStepRuntime);
            }
        }

        public void Load(RecipeDTO recipeDTO)
        {
            for (int i = 0, n = recipeDTO.recipeStepDTOs.Count; i < n; ++i)
            {
                steps[i].Load(recipeDTO.recipeStepDTOs[i]);
            }
        }

        public RecipeStepRuntime GetStep(int index)
        {
            return steps.Get(index);
        }

        public void AddOnRecipeChangedCallback(Action onRecipeChanged)
        {
            onRecipeChangedEvent += onRecipeChanged;
        }

        public void RemoveOnRecipeChangedCallback(Action onRecipeChanged)
        {
            onRecipeChangedEvent -= onRecipeChanged;
        }

        #region Callbacks

        private void OnRecipeStepChanged()
        {
            onRecipeChangedEvent?.Invoke();
        }

        #endregion
    }
}
