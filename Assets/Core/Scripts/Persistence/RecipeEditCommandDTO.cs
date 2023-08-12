using Cooking.Core.Commands;
using System;
using UnityEngine;

namespace Cooking.Core.Persistence
{
    [Serializable]
    public class RecipeEditCommandDTO
    {
        public int type;
        public string data;

        public RecipeEditCommandDTO(RecipeEditCommand command)
        {
            type = (int)command.Type;
            data = JsonUtility.ToJson(command);
        }
    }
}
