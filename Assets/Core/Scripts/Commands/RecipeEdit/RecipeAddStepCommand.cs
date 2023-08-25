using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeAddStepCommand : RecipeEditCommand
    {
        #region Properties and Fields

        public override RecipeEditCommandType CommandType => RecipeEditCommandType.AddStep;

        public int Index => index;

        [SerializeField] private int index = -1;

        #endregion

        public RecipeAddStepCommand() { }

        public RecipeAddStepCommand(int index)
        {
            this.index = index;
        }
    }
}
