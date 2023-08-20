using UnityEngine;
using Celeste.Objects;
using Cooking.Core.Objects;
using Cooking.Core.Record;

namespace Cooking.Core.Catalogue
{
    [CreateAssetMenu(fileName = nameof(RecipeCatalogue), menuName = "Cooking/Core/Recipe Catalogue")]
    public class RecipeCatalogue : ListScriptableObject<Recipe>
    {
        public void AddRecipeImagesTo(ImageRecord imageRecord)
        {
            foreach (Recipe recipe in Items)
            {
                foreach (RecipeStep recipeStep in recipe.Steps)
                {
                    if (recipeStep.HasImages)
                    {
                        foreach (Sprite sprite in recipeStep.Images)
                        {
                            imageRecord.AddItem(sprite.name, sprite);
                        }
                    }
                }
            }
        }
    }
}