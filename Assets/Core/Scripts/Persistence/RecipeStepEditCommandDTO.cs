using Cooking.Core.Commands;
using System;
using UnityEngine;

namespace Cooking.Core.Persistence
{
    [Serializable]
    public class RecipeStepEditCommandDTO
    {
        public int type;
        public string data;

        public RecipeStepEditCommandDTO(RecipeStepEditCommand command)
        {
            type = (int)command.CommandType;
            data = JsonUtility.ToJson(command);
        }
    }
}
