using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Core.Objects
{
    [Serializable]
    public class RecipeStep
    {
        #region Properties and Fields

        public string Title => title;
        public string Description => description;
        public string Tip => tip;
        public string Warning => warning;
        public string Recommendation => recommendation;
        public string Explanation => explanation;
        public bool HasImages => images.Count > 0;
        public IReadOnlyList<Sprite> Images => images;
        public IReadOnlyList<IngredientInfo> Ingredients => ingredients;

        [SerializeField] private string title;
        [SerializeField, TextArea(3, 10)] private string description;
        [SerializeField, TextArea(1, 10)] private string tip;
        [SerializeField, TextArea(1, 10)] private string warning;
        [SerializeField, TextArea(1, 10)] private string recommendation;
        [SerializeField, TextArea(1, 10)] private string explanation;
        [SerializeField] private List<Sprite> images = new List<Sprite>();
        [SerializeField] private List<IngredientInfo> ingredients = new List<IngredientInfo>();

        #endregion

        public RecipeStep(Sprite stepImage)
        {
            title = stepImage.name;
            images.Add(stepImage);
        }
    }
}
