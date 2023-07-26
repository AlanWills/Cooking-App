using Celeste.Parameters;
using Cooking.Core.Runtime;
using UnityEngine;

namespace Cooking.Core.Parameters
{
    [CreateAssetMenu(fileName = nameof(RecipeRuntimeReference), menuName = "Cooking/Parameters/Recipe Runtime Reference")]
    public class RecipeRuntimeReference : ParameterReference<RecipeRuntime, RecipeRuntimeValue, RecipeRuntimeReference>
    {
        #region Operators

        public static bool operator ==(RecipeRuntimeReference reference, RecipeRuntime r)
        {
            return reference.Value == r;
        }

        public static bool operator !=(RecipeRuntimeReference reference, RecipeRuntime r)
        {
            return reference.Value != r;
        }

        #endregion

        #region Equals & HashCode

        public override bool Equals(object obj)
        {
            return obj is RecipeRuntimeReference reference &&
                   base.Equals(obj) &&
                   Value == reference.Value;
        }

        public override int GetHashCode()
        {
            int hashCode = -159790080;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        #endregion
    }
}
