using System;
using UnityEngine;

namespace Cooking.Core.Objects
{
    [Serializable]
    public class RecipeStep
    {
        #region Properties and Fields

        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite image;

        #endregion
    }
}
