using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeStepEditRecommendationCommand : RecipeStepEditCommand
    {
        #region Properties and Fields

        public override RecipeStepEditCommandType CommandType => RecipeStepEditCommandType.EditRecommendation;

        public string Recommendation => recommendation;

        [SerializeField] private string recommendation;

        #endregion

        public RecipeStepEditRecommendationCommand()
        {
        }

        public RecipeStepEditRecommendationCommand(string recommendation)
        {
            this.recommendation = recommendation;
        }
    }
}
