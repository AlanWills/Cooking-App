using UnityEngine;
using Celeste.Events;
using Cooking.Core.Runtime;

namespace Cooking.Core.Events
{
	public class RecipeRuntimeValueChangedEventRaiser : ParameterisedEventRaiser<ValueChangedArgs<RecipeRuntime>, RecipeRuntimeValueChangedEvent> { }
}
