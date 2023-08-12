using System;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeEditAddStepCommand : RecipeEditCommand
    {
        #region Properties and Fields

        public override RecipeEditCommandType Type => RecipeEditCommandType.AddStep;

        #endregion
    }
}
