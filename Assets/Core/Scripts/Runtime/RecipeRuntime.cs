using Celeste.DataStructures;
using Cooking.Core.Objects;
using Cooking.Core.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Core.Runtime
{
    public class RecipeRuntime
    {
        #region Properties and Fields

        public string Guid { get; }
        public string DisplayName { get; private set; }
        public Sprite Thumbnail { get; }
        public int NumSteps => steps.Count;

        private List<RecipeStepRuntime> steps = new List<RecipeStepRuntime>();
        private Action onRecipeChangedEvent;

        #endregion

        public RecipeRuntime(string displayName)
        {
            Guid = System.Guid.NewGuid().ToString();
            DisplayName = displayName;
        }

        public RecipeRuntime(string guid, string displayName)
        {
            Guid = guid;
            DisplayName = displayName;
        }

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
                // TODO: Change this to use the edit commands like the recipe steps so that we build the recipe out of changes
                if (steps.Count <= i)
                {
                    steps.Add(new RecipeStepRuntime());
                }

                steps[i].Load(recipeDTO.recipeStepDTOs[i]);
            }
        }

        public RecipeStepRuntime GetStep(int index)
        {
            return steps.Get(index);
        }

        public RecipeStepRuntime AddStep()
        {
            RecipeStepRuntime recipeStepRuntime = new RecipeStepRuntime();
            steps.Add(recipeStepRuntime);
            onRecipeChangedEvent?.Invoke();

            return recipeStepRuntime;
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
