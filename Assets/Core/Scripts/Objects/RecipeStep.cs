using System;
using UnityEngine;

namespace Cooking.Core.Objects
{
    [Serializable]
    public class RecipeStep
    {
        #region Properties and Fields

        public string Title => title;
        public string Description => description;
        public Sprite Image => image;

        [SerializeField] private string title;
        [SerializeField, TextArea] private string description;
        [SerializeField] private Sprite image;

        #endregion
    }
}
