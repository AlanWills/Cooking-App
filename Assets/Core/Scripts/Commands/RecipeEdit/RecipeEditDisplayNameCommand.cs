using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeEditDisplayNameCommand : RecipeEditCommand
    {
        #region Properties and Fields

        public override RecipeEditCommandType CommandType => RecipeEditCommandType.EditDisplayName;

        public string DisplayName => displayName;

        [SerializeField] private string displayName;

        #endregion

        public RecipeEditDisplayNameCommand()
        {
        }

        public RecipeEditDisplayNameCommand(string displayName)
        {
            this.displayName = displayName;
        }
    }
}
