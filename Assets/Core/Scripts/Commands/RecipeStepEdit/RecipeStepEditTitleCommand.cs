using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeStepEditTitleCommand : RecipeStepEditCommand
    {
        #region Properties and Fields

        public override RecipeStepEditCommandType Type => RecipeStepEditCommandType.EditTitle;

        public string Title => title;

        [SerializeField] private string title;

        #endregion

        public RecipeStepEditTitleCommand()
        {
        }

        public RecipeStepEditTitleCommand(string title)
        {
            this.title = title;
        }
    }
}
