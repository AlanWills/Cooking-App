using Cooking.Core.Runtime;
using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public abstract class RecipeStepEditCommand
    {
        #region Properties and Fields

        public abstract RecipeStepEditCommandType Type { get; }

        #endregion
    }
}
