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
        public bool HasImages => images.Count > 0;
        public IReadOnlyList<Sprite> Images => images;

        [SerializeField] private string title;
        [SerializeField, TextArea(3, 10)] private string description;
        [SerializeField, TextArea(1, 10)] private string tip;
        [SerializeField, TextArea(1, 10)] private string warning;
        [SerializeField, TextArea(1, 10)] private string recommendation;
        [SerializeField, TextArea(1, 10)] private string explanation;
        [SerializeField] private List<Sprite> images = new List<Sprite>();

        #endregion

        public RecipeStep(Sprite stepImage)
        {
            title = stepImage.name;
            images.Add(stepImage);
        }
    }
}
