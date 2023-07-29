using System.Collections.Generic;
using Celeste.Tools;
using PolyAndCode.UI;
using UnityEngine;
using Cooking.Core.Record;
using Cooking.Core.Runtime;

namespace Cooking.Core.UI
{
	public class RecipesUIController : MonoBehaviour, IRecyclableScrollRectDataSource
	{
		#region Properties and Fields

		[SerializeField] private RecyclableScrollRect scrollRect;
		[SerializeField] private RecipeRecord recipeRecord;

		private List<RecipeUIData> recipeCellData = new List<RecipeUIData>();

		#endregion

		#region Unity Methods

		private void OnValidate()
		{
			this.TryGetInChildren(ref scrollRect);
		}

		private void Start()
		{
			SetUpUI();
		}

		#endregion

		private void SetUpUI()
		{
			for (int i = 0, n = recipeRecord.NumRecipes; i < n; ++i)
			{
				RecipeRuntime recipe = recipeRecord.GetRecipe(i);
				recipeCellData.Add(new RecipeUIData(recipe));
			}

			scrollRect.Initialize(this);
		}

		#region IRecyclableScrollRectDataSource

		public int GetItemCount()
		{
			return recipeCellData.Count;
		}

		public void SetCell(ICell cell, int index)
		{
			(cell as RecipeUI).Hookup(recipeCellData[index]);
		}

		#endregion
	}
}
