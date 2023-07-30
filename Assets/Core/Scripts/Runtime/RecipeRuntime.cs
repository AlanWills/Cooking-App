using Celeste.DataStructures;
using Cooking.Core.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Core.Runtime
{
    public class RecipeRuntime
    {
        #region Properties and Fields

        public string DisplayName => displayName;
        public Sprite Thumbnail => thumbnail;
        public int NumSteps => steps.Count;

        [NonSerialized] private string displayName;
        [NonSerialized] private Sprite thumbnail;
        [NonSerialized] private List<RecipeStep> steps = new List<RecipeStep>();

        #endregion

        public RecipeRuntime(Recipe recipe)
        {
            displayName = recipe.DisplayName;
            thumbnail = recipe.Thumbnail;
            steps.AddRange(recipe.Steps);
        }

        public RecipeStep GetStep(int index)
        {
            return steps.Get(index);
        }
    }
}
