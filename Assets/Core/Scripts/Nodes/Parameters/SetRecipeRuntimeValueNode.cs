using Celeste.FSM.Nodes.Parameters;
using Cooking.Core.Parameters;
using Cooking.Core.Runtime;
using System;

namespace Cooking.Core.Nodes
{
    [Serializable]
    [CreateNodeMenu("Cooking/Parameters/Set Recipe Runtime Value")]
    [NodeWidth(250)]
    public class SetRecipeRuntimeValueNode : SetValueNode<RecipeRuntime, RecipeRuntimeValue, RecipeRuntimeReference>
    {
        #region FSM Runtime

        protected override void SetValue(RecipeRuntime newValue)
        {
            value.Value = newValue;
        }

        #endregion
    }
}
