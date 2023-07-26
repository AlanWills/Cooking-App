using System.ComponentModel;
using System;
using Celeste.FSM.Nodes.Events.Conditions;
using Cooking.Core.Runtime;
using Cooking.Core.Events;

namespace Cooking.Core.Nodes
{
    [Serializable]
    [DisplayName("Recipe Runtime")]
    public class RecipeRuntimeEventCondition : ParameterizedEventCondition<RecipeRuntime, RecipeRuntimeEvent>
    {
    }
}
