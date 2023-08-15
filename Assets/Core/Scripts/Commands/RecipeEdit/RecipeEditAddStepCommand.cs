using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeEditAddStepCommand : RecipeEditCommand
    {
        #region Properties and Fields

        public override RecipeEditCommandType Type => RecipeEditCommandType.AddStep;

        public int Index => index;

        [SerializeField] private int index = -1;

        #endregion

        public RecipeEditAddStepCommand() { }

        public RecipeEditAddStepCommand(int index)
        {
            this.index = index;
        }
    }
}
