using Cooking.Core.Catalogue;
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
                    AddCustomEdit(new RecipeStepEditTitleCommand(value));
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
                    AddCustomEdit(new RecipeStepEditDescriptionCommand(value));
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
                    AddCustomEdit(new RecipeStepEditTipCommand(value));
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
                    AddCustomEdit(new RecipeStepEditWarningCommand(value));
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
                    AddCustomEdit(new RecipeStepEditRecommendationCommand(value));
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
                    AddCustomEdit(new RecipeStepEditExplanationCommand(value));
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
        private List<IngredientRuntime> ingredients = new List<IngredientRuntime>();
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

            for (int i = 0, n = recipeStep.Images.Count; i < n; ++i)
            {
                Sprite image = recipeStep.Images[i];
                images.Add(new ImageRuntime(image));
                initialEdits.Add(new RecipeStepAddImageCommand(i, image.name));
            }

            for (int i = 0, n = recipeStep.Ingredients.Count; i < n; ++i)
            {
                IngredientInfo ingredientInfo = recipeStep.Ingredients[i];
                ingredients.Add(new IngredientRuntime(ingredientInfo));
                initialEdits.Add(new RecipeStepAddIngredientCommand(i, ingredientInfo));
            }
        }

        public void Load(RecipeStepDTO recipeStepDTO, IngredientCatalogue ingredientCatalogue)
        {
            foreach (RecipeStepEditCommandDTO editCommandDTO in recipeStepDTO.edits)
            {
                LoadEdit(editCommandDTO, ingredientCatalogue);
            }
        }

        private void LoadEdit(RecipeStepEditCommandDTO editCommandDTO, IngredientCatalogue ingredientCatalogue)
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
                        images.Insert(addImage.Index, new ImageRuntime(addImage.ImageId));
                        customEdits.Add(addImage);
                    }
                    break;

                case RecipeStepEditCommandType.AddIngredient:
                    {
                        RecipeStepAddIngredientCommand addIngredient = CommandFactory.Create<RecipeStepAddIngredientCommand>(editCommandDTO.data);

                        if (ingredientCatalogue.TryFindByGuid(addIngredient.Guid, out var ingredient))
                        {
                            ingredients.Insert(addIngredient.Index, new IngredientRuntime(ingredient, addIngredient.Unit, addIngredient.Type, addIngredient.Quantity, addIngredient.Optional));
                            customEdits.Add(addIngredient);
                        }
                        else
                        {
                            UnityEngine.Debug.LogAssertion($"Could not find ingredient in catalogue with guid {addIngredient.Guid}.");
                        }
                    }
                    break;

                default:
                    UnityEngine.Debug.LogAssertion($"Could not find suitable {nameof(RecipeStepEditCommand)} for type: {editCommandDTO.type}.");
                    break;
            }
        }

        private void AddCustomEdit(RecipeStepEditCommand editCommand)
        {
            customEdits.Add(editCommand);
            onRecipeStepChangedEvent?.Invoke();
        }

        public void AddImage(Sprite image)
        {
            images.Add(new ImageRuntime(image));
            AddCustomEdit(new RecipeStepAddImageCommand(images.Count - 1, image.name));
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
