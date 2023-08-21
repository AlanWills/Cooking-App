using Cooking.Core.Enums;
using Cooking.Core.Objects;
using System;
using UnityEngine;

namespace Cooking.Core.Commands
{
    [Serializable]
    public class RecipeEditAddIngredientCommand : RecipeEditCommand
    {
        #region Properties and Fields

        public override RecipeEditCommandType CommandType => RecipeEditCommandType.AddIngredient;

        public string Guid => guid;
        public int Index => index;
        public MeasurementUnit Unit => unit;
        public MeasurementType Type => type;
        public float Quantity => quantity;
        public bool Optional => optional;

        [SerializeField] private int index = -1;
        [SerializeField] private string guid;
        [SerializeField] private MeasurementUnit unit;
        [SerializeField] private MeasurementType type;
        [SerializeField] private float quantity;
        [SerializeField] private bool optional;

        #endregion

        public RecipeEditAddIngredientCommand() { }

        public RecipeEditAddIngredientCommand(int index, IngredientInfo quantity)
            : this(
                  index, 
                  quantity.Ingredient.Guid, 
                  quantity.Unit, 
                  quantity.Type,
                  quantity.Quantity,
                  quantity.Optional)
        {
        }

        public RecipeEditAddIngredientCommand(
            int index, 
            string guid, 
            MeasurementUnit unit,
            MeasurementType type,
            float quantity,
            bool optional)
        {
            this.guid = guid;
            this.index = index;
            this.unit = unit;
            this.type = type;
            this.quantity = quantity;
            this.optional = optional;
        }
    }
}
