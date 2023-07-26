using Cooking.Core.Runtime;

namespace Cooking.Core.UI
{
	public class RecipeUIData
	{
		#region Properties and Fields

		public RecipeRuntime Recipe { get; }

		#endregion

		public RecipeUIData(RecipeRuntime recipe)
		{
			Recipe = recipe;
		}
	}
}
