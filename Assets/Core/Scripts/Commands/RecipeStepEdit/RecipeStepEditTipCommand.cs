using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeStepEditTipCommand : RecipeStepEditCommand
    {
        #region Properties and Fields

        public override RecipeStepEditCommandType Type => RecipeStepEditCommandType.EditTip;

        public string Tip => tip;

        [SerializeField] private string tip;

        #endregion

        public RecipeStepEditTipCommand()
        {
        }

        public RecipeStepEditTipCommand(string tip)
        {
            this.tip = tip;
        }
    }
}
