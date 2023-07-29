using Celeste.Parameters;
using Cooking.Core.Events;
using Cooking.Core.Runtime;
using UnityEngine;

namespace Cooking.Core.Parameters
{
    [CreateAssetMenu(fileName = nameof(RecipeRuntimeValue), menuName = "Cooking/Parameters/Recipe Runtime Value")]
    public class RecipeRuntimeValue : ParameterValue<RecipeRuntime, RecipeRuntimeValueChangedEvent>
    {
        #region Operators

        public static bool operator ==(RecipeRuntimeValue value, RecipeRuntime r)
        {
            return value.Value == r;
        }

        public static bool operator !=(RecipeRuntimeValue value, RecipeRuntime r)
        {
            return value.Value != r;
        }

        #endregion

        #region Equals & HashCode

        public override bool Equals(object obj)
        {
            return obj is RecipeRuntimeValue value &&
                   base.Equals(obj) &&
                   Value == value.Value;
        }

        public override int GetHashCode()
        {
            int hashCode = -159790080;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = Value != null ? hashCode * -1521134295 + Value.GetHashCode() : 0;
            return hashCode;
        }

        #endregion
    }
}
