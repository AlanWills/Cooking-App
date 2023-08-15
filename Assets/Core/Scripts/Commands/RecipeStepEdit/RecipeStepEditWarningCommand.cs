using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeStepEditWarningCommand : RecipeStepEditCommand
    {
        #region Properties and Fields

        public override RecipeStepEditCommandType Type => RecipeStepEditCommandType.EditWarning;

        public string Warning => warning;

        [SerializeField] private string warning;

        #endregion

        public RecipeStepEditWarningCommand()
        {
        }

        public RecipeStepEditWarningCommand(string warning)
        {
            this.warning = warning;
        }
    }
}
