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

        public IReadOnlyList<RecipeEditCommand> InitialEdits => initialEdits;
        public bool HasCustomEdits => customEdits.Count > 0 || steps.Exists(x => x.HasCustomEdits);
        public IReadOnlyList<RecipeEditCommand> CustomEdits => customEdits;

        private string displayName;
        private List<RecipeEditCommand> initialEdits = new List<RecipeEditCommand>();
        private List<RecipeEditCommand> customEdits = new List<RecipeEditCommand>();
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
            Thumbnail = recipe.Thumbnail;
            
            displayName = recipe.DisplayName;
            initialEdits.Add(new RecipeEditDisplayNameCommand(displayName));

            for (int i = 0, n = recipe.Steps.Count; i < n; ++i)
            {
                RecipeStep recipeStep  = recipe.Steps[i];
                RecipeStepRuntime recipeStepRuntime = new RecipeStepRuntime(recipeStep);
                recipeStepRuntime.AddOnRecipeStepChangedCallback(OnRecipeStepChanged);
                steps.Add(recipeStepRuntime);
                initialEdits.Add(new RecipeEditAddStepCommand(i));
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
                    customEdits.Add(editDisplayName);
                    break;

                case RecipeEditCommandType.AddStep:
                    RecipeEditAddStepCommand addStep = CommandFactory.Create<RecipeEditAddStepCommand>(editCommandDTO.data);
                    InsertStepImpl(addStep.Index);
                    customEdits.Add(addStep);
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
            return InsertStep(NumSteps);
        }

        public void RemoveStep(int index)
        {
            steps.RemoveAt(index);
        }

        public RecipeStepRuntime InsertStep(int index)
        {
            RecipeStepRuntime recipeStepRuntime = InsertStepImpl(index);
            customEdits.Add(new RecipeEditAddStepCommand(index));
            onRecipeChangedEvent?.Invoke();

            return recipeStepRuntime;
        }

        private RecipeStepRuntime InsertStepImpl(int index)
        {
            RecipeStepRuntime recipeStepRuntime = new RecipeStepRuntime();

            if (index >= steps.Count)
            {
                steps.Add(recipeStepRuntime);
            }
            else
            {
                steps.Insert(index, recipeStepRuntime);
            }

            return recipeStepRuntime;
        }

        private void AddEdit(RecipeEditCommand editCommand)
        {
            customEdits.Add(editCommand);
            onRecipeChangedEvent?.Invoke();
        }

        #region Callbacks

        private void OnRecipeStepChanged()
        {
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

        #endregion
    }
}
