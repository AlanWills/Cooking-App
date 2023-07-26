using Cooking.Core.Objects;
using System;
using UnityEngine;

namespace Cooking.Core.Runtime
{
    public class RecipeRuntime
    {
        #region Properties and Fields

        public string DisplayName => recipe.DisplayName;
        public Sprite Thumbnail => recipe.Thumbnail;

        [NonSerialized] private Recipe recipe;

        #endregion

        public RecipeRuntime(Recipe recipe)
        {
            this.recipe = recipe;
        }
    }
}
