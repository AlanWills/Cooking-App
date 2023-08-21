using System;

namespace Cooking.Core.Commands
{
    [Serializable]
    public abstract class RecipeEditCommand
    {
        #region Properties and Fields

        public abstract RecipeEditCommandType CommandType { get; }

        #endregion
    }
}
