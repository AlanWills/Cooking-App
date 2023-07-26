using System;
using UnityEngine;
using Celeste.Events;
using Cooking.Core.Runtime;

namespace Cooking.Core.Events 
{
	[Serializable]
	public class RecipeRuntimeValueChangedUnityEvent : ValueChangedUnityEvent<RecipeRuntime> { }

	[Serializable]
	[CreateAssetMenu(fileName = nameof(RecipeRuntimeValueChangedEvent), menuName = "Cooking/Events/Core/Recipe Runtime Value Changed Event")]
	public class RecipeRuntimeValueChangedEvent : ParameterisedValueChangedEvent<RecipeRuntime> { }
	
	[Serializable]
	public class GuaranteedRecipeRuntimeValueChangedEvent : GuaranteedParameterisedValueChangedEvent<RecipeRuntimeValueChangedEvent, RecipeRuntime> { }
}
