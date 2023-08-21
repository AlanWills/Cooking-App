using UnityEngine;
using Celeste.Objects;
using Cooking.Core.Objects;

namespace Cooking.Core.Catalogue
{
    [CreateAssetMenu(fileName = nameof(RecipeCatalogue), menuName = "Cooking/Core/Recipe Catalogue")]
    public class RecipeCatalogue : ListScriptableObject<Recipe>
    {
        public void AddRecipeImagesTo(ImageCatalogue imageCatalogue)
        {
            foreach (Recipe recipe in Items)
            {
                foreach (RecipeStep recipeStep in recipe.Steps)
                {
                    if (recipeStep.HasImages)
                    {
                        foreach (Sprite sprite in recipeStep.Images)
                        {
                            imageCatalogue.AddItem(sprite);
                        }
                    }
                }
            }
        }
    }
}