using Celeste.Scene;
using Celeste.Scene.Events;
using Cooking.Core.Parameters;
using UnityEngine;

namespace Cooking.Instructions.Contexts
{
    [CreateAssetMenu(fileName = nameof(InstructionsContextProvider), menuName = "Cooking/Contexts/Instructions Context Provider")]
    public class InstructionsContextProvider : ContextProvider
    {
        [SerializeField] private RecipeRuntimeValue selectedRecipe;

        public override Context Create()
        {
            return new InstructionsContext
            {
                recipe = selectedRecipe.Value
            };
        }
    }
}
