using System;
using UnityEngine;
using UnityEngine.Events;
using Celeste.Events;
using Cooking.Core.Runtime;

namespace Cooking.Core.Events
{
	[Serializable]
	public class RecipeRuntimeUnityEvent : UnityEvent<RecipeRuntime> { }
	
	[Serializable]
	[CreateAssetMenu(fileName = nameof(RecipeRuntimeEvent), menuName = "Cooking/Events/Core/Recipe Runtime Event")]
	public class RecipeRuntimeEvent : ParameterisedEvent<RecipeRuntime> { }
	
	[Serializable]
	public class GuaranteedRecipeRuntimeEvent : GuaranteedParameterisedEvent<RecipeRuntimeEvent, RecipeRuntime> { }
}
