using UnityEngine;

namespace Cooking.Core.Commands
{
    public static class RecipeStepEditCommandFactory
    {
        public static T Create<T>(string data) where T : class, new()
        {
            T editCommand = new T();
            JsonUtility.FromJsonOverwrite(data, editCommand);
            return editCommand;
        }
    }
}
