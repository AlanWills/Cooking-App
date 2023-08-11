using Celeste.DataStructures;
using Cooking.Core.Events;
using Cooking.Core.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cooking.Core.Record
{
    [CreateAssetMenu(fileName = nameof(RecipeRecord), menuName = "Cooking/Core/Recipe Record")]
    public class RecipeRecord : ScriptableObject
    {
        #region Properties and Fields

        public int NumRecipes => recipes.Count;

        [SerializeField] private Celeste.Events.Event saveRecipeRecordEvent;
        [SerializeField] private GuaranteedRecipeRuntimeEvent onRecipeAddedEvent;

        [NonSerialized] private List<RecipeRuntime> recipes = new List<RecipeRuntime>();

        #endregion

        public void AddRecipe(RecipeRuntime recipe)
        {
            recipes.Add(recipe);
            onRecipeAddedEvent.Invoke(recipe);
            saveRecipeRecordEvent.Invoke();
        }

        public RecipeRuntime FindRecipe(Predicate<RecipeRuntime> predicate)
        {
            return recipes.Find(predicate);
        }

        public RecipeRuntime GetRecipe(int index)
        {
            return recipes.Get(index);
        }

        public void AddOnRecipeAddedCallback(UnityAction<RecipeRuntime> callback)
        {
            onRecipeAddedEvent.AddListener(callback);
        }

        public void RemoveOnRecipeAddedCallback(UnityAction<RecipeRuntime> callback)
        {
            onRecipeAddedEvent.RemoveListener(callback);
        }
    }
}