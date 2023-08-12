using UnityEngine;

namespace Cooking.Core.Commands
{
    public static class CommandFactory
    {
        public static T Create<T>(string data) where T : class, new()
        {
            T command = new T();
            JsonUtility.FromJsonOverwrite(data, command);
            return command;
        }
    }
}
