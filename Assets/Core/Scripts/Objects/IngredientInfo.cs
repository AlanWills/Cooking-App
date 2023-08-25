using Cooking.Core.Enums;
using System;

namespace Cooking.Core.Objects
{
    [Serializable]
    public struct IngredientInfo
    {
        public Ingredient Ingredient;
        public MeasurementUnit Unit;
        public MeasurementType Type;
        public float Quantity;
        public bool Optional;
    }
}
