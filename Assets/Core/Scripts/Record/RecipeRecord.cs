using Celeste.DataStructures;
using Cooking.Core.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Core.Record
{
    [CreateAssetMenu(fileName = nameof(RecipeRecord), menuName = "Cooking/Core/Recipe Record")]
    public class RecipeRecord : ScriptableObject
    {
        #region Properties and Fields

        public int NumRecipes => recipes.Count;

        [NonSerialized] private List<RecipeRuntime> recipes = new List<RecipeRuntime>();

        #endregion

        public void AddRecipe(RecipeRuntime recipe)
        {
            recipes.Add(recipe);
        }

        public RecipeRuntime FindRecipe(Predicate<RecipeRuntime> predicate)
        {
            return recipes.Find(predicate);
        }

        public RecipeRuntime GetRecipe(int index)
        {
            return recipes.Get(index);
        }
    }
}