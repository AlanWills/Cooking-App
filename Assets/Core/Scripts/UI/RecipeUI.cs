using UnityEngine;
using PolyAndCode.UI;
using Cooking.Core.Runtime;
using TMPro;
using UnityEngine.UI;
using Cooking.Core.Events;

namespace Cooking.Core.UI
{
	public class RecipeUI : MonoBehaviour, ICell
	{
		#region Properties and Fields

		[Header("UI Elements")]
		[SerializeField] private TextMeshProUGUI recipeName;
		[SerializeField] private Image recipeThumbnail;

		[Header("Events")]
		[SerializeField] private RecipeRuntimeEvent startRecipe;

		private RecipeRuntime recipe;

		#endregion

		public void Hookup(RecipeUIData recipeUIData)
		{
			recipe = recipeUIData.Recipe;
			recipeName.text = recipe.DisplayName;
			recipeThumbnail.sprite = recipe.Thumbnail;
		}

		#region Unity Methods

		private void OnDisable()
		{
			recipe = null;
		}

		#endregion

		#region Callbacks

		public void OnStartRecipe()
		{
			startRecipe.Invoke(recipe);
		}

		#endregion
	}
}
