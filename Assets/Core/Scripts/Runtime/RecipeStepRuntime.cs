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

        public string Title { get; }

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

        public string Tip { get; private set; }
        public string Warning { get; private set; }
        public string Recommendation { get; private set; }
        public string Explanation { get; }
        public bool HasImages => images.Count > 0;
        public IReadOnlyList<ImageRuntime> Images => images;
        public IReadOnlyList<RecipeStepEditCommand> Edits => edits;

        private string description;
        private List<ImageRuntime> images = new List<ImageRuntime>();
        private List<RecipeStepEditCommand> edits = new List<RecipeStepEditCommand>();
        private Action onRecipeStepChangedEvent;

        #endregion

        public RecipeStepRuntime() { }

        public RecipeStepRuntime(RecipeStep recipeStep)
        {
            Title = recipeStep.Title;
            description= recipeStep.Description;
            Tip = recipeStep.Tip;
            Warning = recipeStep.Warning;
            Recommendation = recipeStep.Recommendation;
            Explanation = recipeStep.Explanation;

            foreach (Sprite image in recipeStep.Images)
            {
                images.Add(new ImageRuntime(image));
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
                case RecipeStepEditCommandType.EditDescription:
                    RecipeStepEditDescriptionCommand editDescription = CommandFactory.Create<RecipeStepEditDescriptionCommand>(editCommandDTO.data);
                    description = editDescription.Description;
                    edits.Add(editDescription);
                    break;

                case RecipeStepEditCommandType.AddImage:
                    RecipeStepAddImageCommand addImage = CommandFactory.Create<RecipeStepAddImageCommand>(editCommandDTO.data);
                    images.Add(new ImageRuntime(addImage.ImageId));
                    edits.Add(addImage);
                    break;

                default:
                    UnityEngine.Debug.LogAssertion($"Could not find suitable {nameof(RecipeStepEditCommand)} for type: {editCommandDTO.type}.");
                    break;
            }
        }

        private void AddEdit(RecipeStepEditCommand editCommand)
        {
            edits.Add(editCommand);
            onRecipeStepChangedEvent?.Invoke();
        }

        public void AddImage(Sprite image)
        {
            images.Add(new ImageRuntime(image));
            AddEdit(new RecipeStepAddImageCommand(image.name));
        }

        public void AddOnRecipeStepChangedCallback(Action onRecipeChanged)
        {
            onRecipeStepChangedEvent += onRecipeChanged;
        }

        public void RemoveOnRecipeStepChangedCallback(Action onRecipeChanged)
        {
            onRecipeStepChangedEvent -= onRecipeChanged;
        }
    }
}
