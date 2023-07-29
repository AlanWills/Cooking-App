using Celeste.Parameters;
using Celeste.Scene.Events;
using Cooking.Core.Parameters;
using Cooking.Instructions.Contexts;
using UnityEngine;

namespace Cooking.Instructions.Managers
{
    [AddComponentMenu("Cooking/Instructions Manager")]
    public class InstructionsManager : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private RecipeRuntimeValue currentRecipe;
        [SerializeField] private IntValue currentRecipeStep;

        #endregion

        #region Callbacks

        public void OnInstructionsContextLoaded(OnContextLoadedArgs onContextLoadedArgs)
        {
            InstructionsContext context = onContextLoadedArgs.context as InstructionsContext;
            currentRecipe.Value = context.recipe;
            currentRecipeStep.Value = 0;
        }

        public void OnNextStep()
        {
            ++currentRecipeStep.Value;
        }

        public void OnPreviousStep()
        {
            --currentRecipeStep.Value;
        }

        #endregion
    }
}
