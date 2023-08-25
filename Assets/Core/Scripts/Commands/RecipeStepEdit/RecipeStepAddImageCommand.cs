using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeStepAddImageCommand : RecipeStepEditCommand
    {
        #region Properties and Fields

        public override RecipeStepEditCommandType CommandType => RecipeStepEditCommandType.AddImage;

        public int Index => index;
        public string ImageId => imageId;

        [SerializeField] private int index;
        [SerializeField] private string imageId;

        #endregion

        public RecipeStepAddImageCommand()
        {
        }

        public RecipeStepAddImageCommand(int index, string imageId)
        {
            this.index = index;
            this.imageId = imageId;
        }
    }
}
