using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeStepEditDescriptionCommand : RecipeStepEditCommand
    {
        #region Properties and Fields

        public override RecipeStepEditCommandType CommandType => RecipeStepEditCommandType.EditDescription;

        public string Description => description;

        [SerializeField] private string description;

        #endregion

        public RecipeStepEditDescriptionCommand()
        {
        }

        public RecipeStepEditDescriptionCommand(string description)
        {
            this.description = description;
        }
    }
}
