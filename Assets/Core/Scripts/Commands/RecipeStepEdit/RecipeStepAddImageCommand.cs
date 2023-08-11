using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeStepAddImageCommand : RecipeStepEditCommand
    {
        #region Properties and Fields

        public override RecipeStepEditCommandType Type => RecipeStepEditCommandType.AddImage;

        public string ImageId => imageId;

        [SerializeField] private string imageId;

        #endregion

        public RecipeStepAddImageCommand()
        {
        }

        public RecipeStepAddImageCommand(string imageId)
        {
            this.imageId = imageId;
        }
    }
}
