using Celeste.DataStructures;
using Cooking.Core.Commands;
using Cooking.Core.Objects;
using Cooking.Core.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Core.Runtime
{
    public class RecipeRuntime
    {
        #region Properties and Fields

        public string Guid { get; }
        
        public string DisplayName
        {
            get => displayName;
            set
            {
                if (string.CompareOrdinal(displayName, value) != 0)
                {
                    displayName = value;
                    AddEdit(new RecipeEditDisplayNameCommand(value));
                }
            }
        }

        public Sprite Thumbnail { get; }
        public int NumSteps => steps.Count;
        public IReadOnlyList<RecipeEditCommand> Edits => edits;

        private string displayName;
        private List<RecipeEditCommand> edits = new List<RecipeEditCommand>();
        private List<RecipeStepRuntime> steps = new List<RecipeStepRuntime>();
        private Action onRecipeChangedEvent;

        #endregion

        public RecipeRuntime(string displayName)
        {
            Guid = System.Guid.NewGuid().ToString();
            DisplayName = displayName;
        }

        public RecipeRuntime(string guid, string displayName)
        {
            Guid = guid;
            DisplayName = displayName;
        }

        public RecipeRuntime(Recipe recipe)
        {
            Guid = recipe.Guid;
            DisplayName = recipe.DisplayName;
            Thumbnail = recipe.Thumbnail;

            foreach (RecipeStep recipeStep in recipe.Steps)
            {
                RecipeStepRuntime recipeStepRuntime = new RecipeStepRuntime(recipeStep);
                recipeStepRuntime.AddOnRecipeStepChangedCallback(OnRecipeStepChanged);
                steps.Add(recipeStepRuntime);
            }
        }

        public void Load(RecipeDTO recipeDTO)
        {
            foreach (RecipeEditCommandDTO editCommandDTO in recipeDTO.edits)
            {
                LoadEdit(editCommandDTO);
            }

            for (int i = 0, n = recipeDTO.recipeStepDTOs.Count; i < n; ++i)
            {
                steps[i].Load(recipeDTO.recipeStepDTOs[i]);
            }
        }

        private void LoadEdit(RecipeEditCommandDTO editCommandDTO)
        {
            switch ((RecipeEditCommandType)editCommandDTO.type)
            {
                case RecipeEditCommandType.EditDisplayName:
                    RecipeEditDisplayNameCommand editDisplayName = CommandFactory.Create<RecipeEditDisplayNameCommand>(editCommandDTO.data);
                    displayName = editDisplayName.DisplayName;
                    edits.Add(editDisplayName);
                    break;

                case RecipeEditCommandType.AddStep:
                    RecipeEditAddStepCommand addStep = CommandFactory.Create<RecipeEditAddStepCommand>(editCommandDTO.data);
                    steps.Add(new RecipeStepRuntime());
                    edits.Add(addStep);
                    break;

                default:
                    UnityEngine.Debug.LogAssertion($"Could not find suitable {nameof(RecipeEditCommand)} for type: {editCommandDTO.type}.");
                    break;
            }
        }

        public RecipeStepRuntime GetStep(int index)
        {
            return steps.Get(index);
        }

        public RecipeStepRuntime AddStep()
        {
            RecipeStepRuntime recipeStepRuntime = new RecipeStepRuntime();
            steps.Add(recipeStepRuntime);
            edits.Add(new RecipeEditAddStepCommand());
            onRecipeChangedEvent?.Invoke();

            return recipeStepRuntime;
        }

        private void AddEdit(RecipeEditCommand editCommand)
        {
            edits.Add(editCommand);
            onRecipeChangedEvent?.Invoke();
        }

        public void AddOnRecipeChangedCallback(Action onRecipeChanged)
        {
            onRecipeChangedEvent += onRecipeChanged;
        }

        public void RemoveOnRecipeChangedCallback(Action onRecipeChanged)
        {
            onRecipeChangedEvent -= onRecipeChanged;
        }

        #region Callbacks

        private void OnRecipeStepChanged()
        {
            onRecipeChangedEvent?.Invoke();
        }

        #endregion
    }
}
