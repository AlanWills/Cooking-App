using UnityEngine;
using Celeste.Events;
using Cooking.Core.Runtime;

namespace Cooking.Core.Events
{
	public class RecipeRuntimeValueChangedEventListener : ParameterisedEventListener<ValueChangedArgs<RecipeRuntime>, RecipeRuntimeValueChangedEvent, RecipeRuntimeValueChangedUnityEvent> { }
}
