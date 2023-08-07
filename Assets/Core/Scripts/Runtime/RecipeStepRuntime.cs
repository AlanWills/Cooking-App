using Cooking.Core.Objects;
using Cooking.Core.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Core.Runtime
{
    public class RecipeStepRuntime
    {
        #region Properties and Fields

        public string Title { get; set; }

        public string Description
        {
            get => description;
            set
            {
                if (string.CompareOrdinal(value, description) != 0)
                {
                    description = value;
                    onRecipeStepChangedEvent?.Invoke();
                }
            }
        }

        public string Tip { get; set; }
        public string Warning { get; set; }
        public string Recommendation { get; set; }
        public string Explanation { get; set; }
        public bool HasImages => images.Count > 0;
        public IReadOnlyList<Sprite> Images => images;

        private string description;
        private List<Sprite> images = new List<Sprite>();
        private Action onRecipeStepChangedEvent;

        #endregion

        public RecipeStepRuntime(RecipeStep recipeStep)
        {
            Title = recipeStep.Title;
            Description= recipeStep.Description;
            Tip = recipeStep.Tip;
            Warning = recipeStep.Warning;
            Recommendation = recipeStep.Recommendation;
            Explanation = recipeStep.Explanation;

            images.AddRange(recipeStep.Images);
        }

        public void Load(RecipeStepDTO recipeStepDTO)
        {
            description = recipeStepDTO.description;
        }

        public void AddOnRecipeStepChangedCallback(Action onRecipeChanged)
        {
            onRecipeStepChangedEvent += onRecipeChanged;
        }

        public void RemoveOnRecipeStepChangedCallback(Action onRecipeChanged)
        {
            onRecipeStepChangedEvent -= onRecipeChanged;
        }
    }
}
