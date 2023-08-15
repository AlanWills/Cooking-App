using Cooking.Core.Commands;
using Cooking.Core.Objects;
using Cooking.Core.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Core.Runtime
{
    public class RecipeStepRuntime
    {
        #region Properties and Fields

        public string Title
        {
            get => title;
            set
            {
                if (string.CompareOrdinal(title, value) != 0)
                {
                    title = value;
                    AddEdit(new RecipeStepEditTitleCommand(value));
                }
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (string.CompareOrdinal(description, value) != 0)
                {
                    description = value;
                    AddEdit(new RecipeStepEditDescriptionCommand(value));
                }
            }
        }

        public string Tip
        {
            get => tip;
            set
            {
                if (string.CompareOrdinal(tip, value) != 0)
                {
                    tip = value;
                    AddEdit(new RecipeStepEditTipCommand(value));
                }
            }
        }

        public string Warning
        {
            get => warning;
            set
            {
                if (string.CompareOrdinal(warning, value) != 0)
                {
                    warning = value;
                    AddEdit(new RecipeStepEditWarningCommand(value));
                }
            }
        }

        public string Recommendation
        {
            get => recommendation;
            set
            {
                if (string.CompareOrdinal(recommendation, value) != 0)
                {
                    recommendation = value;
                    AddEdit(new RecipeStepEditRecommendationCommand(value));
                }
            }
        }

        public string Explanation
        {
            get => explanation;
            set
            {
                if (string.CompareOrdinal(explanation, value) != 0)
                {
                    explanation = value;
                    AddEdit(new RecipeStepEditExplanationCommand(value));
                }
            }
        }

        public bool HasImages => images.Count > 0;
        public IReadOnlyList<ImageRuntime> Images => images;

        public IReadOnlyList<RecipeStepEditCommand> InitialEdits => initialEdits;
        public bool HasCustomEdits => customEdits.Count > 0;
        public IReadOnlyList<RecipeStepEditCommand> CustomEdits => customEdits;

        private string title;
        private string description;
        private string tip;
        private string warning;
        private string recommendation;
        private string explanation;
        private List<ImageRuntime> images = new List<ImageRuntime>();
        private List<RecipeStepEditCommand> initialEdits = new List<RecipeStepEditCommand>();
        private List<RecipeStepEditCommand> customEdits = new List<RecipeStepEditCommand>();
        private Action onRecipeStepChangedEvent;

        #endregion

        public RecipeStepRuntime() { }

        public RecipeStepRuntime(RecipeStep recipeStep)
        {
            title = recipeStep.Title;
            initialEdits.Add(new RecipeStepEditTitleCommand(title));

            description = recipeStep.Description;
            initialEdits.Add(new RecipeStepEditDescriptionCommand(description));

            tip = recipeStep.Tip;
            initialEdits.Add(new RecipeStepEditTipCommand(tip));

            warning = recipeStep.Warning;
            initialEdits.Add(new RecipeStepEditWarningCommand(warning));

            recommendation = recipeStep.Recommendation;
            initialEdits.Add(new RecipeStepEditRecommendationCommand(recommendation));

            explanation = recipeStep.Explanation;
            initialEdits.Add(new RecipeStepEditExplanationCommand(explanation));

            foreach (Sprite image in recipeStep.Images)
            {
                images.Add(new ImageRuntime(image));
                initialEdits.Add(new RecipeStepAddImageCommand(image.name));
            }
        }

        public void Load(RecipeStepDTO recipeStepDTO)
        {
            foreach (RecipeStepEditCommandDTO editCommandDTO in recipeStepDTO.edits)
            {
                LoadEdit(editCommandDTO);
            }
        }

        private void LoadEdit(RecipeStepEditCommandDTO editCommandDTO)
        {
            switch ((RecipeStepEditCommandType)editCommandDTO.type)
            {
                case RecipeStepEditCommandType.EditTitle:
                    {
                        RecipeStepEditTitleCommand editTitle = CommandFactory.Create<RecipeStepEditTitleCommand>(editCommandDTO.data);
                        title = editTitle.Title;
                        customEdits.Add(editTitle);
                    }
                    break;

                case RecipeStepEditCommandType.EditDescription:
                    {
                        RecipeStepEditDescriptionCommand editDescription = CommandFactory.Create<RecipeStepEditDescriptionCommand>(editCommandDTO.data);
                        description = editDescription.Description;
                        customEdits.Add(editDescription);
                    }
                    break;

                case RecipeStepEditCommandType.EditTip:
                    {
                        RecipeStepEditTipCommand editTip = CommandFactory.Create<RecipeStepEditTipCommand>(editCommandDTO.data);
                        tip = editTip.Tip;
                        customEdits.Add(editTip);
                    }
                    break;

                case RecipeStepEditCommandType.EditWarning:
                    {
                        RecipeStepEditWarningCommand editWarning = CommandFactory.Create<RecipeStepEditWarningCommand>(editCommandDTO.data);
                        warning = editWarning.Warning;
                        customEdits.Add(editWarning);
                    }
                    break;

                case RecipeStepEditCommandType.EditRecommendation:
                    {
                        RecipeStepEditRecommendationCommand editRecommendation = CommandFactory.Create<RecipeStepEditRecommendationCommand>(editCommandDTO.data);
                        recommendation = editRecommendation.Recommendation;
                        customEdits.Add(editRecommendation);
                    }
                    break;

                case RecipeStepEditCommandType.EditExplanation:
                    {
                        RecipeStepEditExplanationCommand editExplanation = CommandFactory.Create<RecipeStepEditExplanationCommand>(editCommandDTO.data);
                        explanation = editExplanation.Explanation;
                        customEdits.Add(editExplanation);
                    }
                    break;

                case RecipeStepEditCommandType.AddImage:
                    {
                        RecipeStepAddImageCommand addImage = CommandFactory.Create<RecipeStepAddImageCommand>(editCommandDTO.data);
                        images.Add(new ImageRuntime(addImage.ImageId));
                        customEdits.Add(addImage);
                    }
                    break;

                default:
                    UnityEngine.Debug.LogAssertion($"Could not find suitable {nameof(RecipeStepEditCommand)} for type: {editCommandDTO.type}.");
                    break;
            }
        }

        private void AddEdit(RecipeStepEditCommand editCommand)
        {
            customEdits.Add(editCommand);
            onRecipeStepChangedEvent?.Invoke();
        }

        public void AddImage(Sprite image)
        {
            images.Add(new ImageRuntime(image));
            AddEdit(new RecipeStepAddImageCommand(image.name));
        }

        #region Callbacks

        public void AddOnRecipeStepChangedCallback(Action onRecipeChanged)
        {
            onRecipeStepChangedEvent += onRecipeChanged;
        }

        public void RemoveOnRecipeStepChangedCallback(Action onRecipeChanged)
        {
            onRecipeStepChangedEvent -= onRecipeChanged;
        }

        #endregion
    }
}
