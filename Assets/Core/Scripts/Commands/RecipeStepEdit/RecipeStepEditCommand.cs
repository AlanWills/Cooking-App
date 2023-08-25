using System;

namespace Cooking.Core.Commands
{
    [Serializable]
    public abstract class RecipeStepEditCommand
    {
        #region Properties and Fields

        public abstract RecipeStepEditCommandType CommandType { get; }

        #endregion
    }
}
