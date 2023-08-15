using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeStepEditExplanationCommand : RecipeStepEditCommand
    {
        #region Properties and Fields

        public override RecipeStepEditCommandType Type => RecipeStepEditCommandType.EditExplanation;

        public string Explanation => explanation;

        [SerializeField] private string explanation;

        #endregion

        public RecipeStepEditExplanationCommand()
        {
        }

        public RecipeStepEditExplanationCommand(string explanation)
        {
            this.explanation = explanation;
        }
    }
}
