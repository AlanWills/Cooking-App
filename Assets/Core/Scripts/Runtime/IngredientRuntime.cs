using Cooking.Core.Enums;
using Cooking.Core.Objects;
using UnityEngine;

namespace Cooking.Core.Runtime
{
    public class IngredientRuntime
    {
        #region Properties and Fields

        public string DisplayName => ingredient.DisplayName;
        public MeasurementUnit Unit => unit;
        public MeasurementType Type => type;
        public float Quantity => quantity;
        public bool Optional => optional;

        private Ingredient ingredient;
        private MeasurementUnit unit;
        private MeasurementType type;
        private float quantity;
        private bool optional;

        #endregion

        public IngredientRuntime(IngredientInfo ingredientQuantity)
            : this(
                  ingredientQuantity.Ingredient,
                  ingredientQuantity.Unit,
                  ingredientQuantity.Type,
                  ingredientQuantity.Quantity,
                  ingredientQuantity.Optional)
        {
        }

        public IngredientRuntime(
            Ingredient ingredient,
            MeasurementUnit unit,
            MeasurementType type,
            float quantity,
            bool optional)
        {
            this.ingredient = ingredient;
            this.unit = unit;
            this.type = type;
            this.quantity = quantity;
            this.optional = optional;
        }
    }
}
