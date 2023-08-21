using Celeste.Objects;
using UnityEngine;

namespace Cooking.Core.Objects
{
    [CreateAssetMenu(fileName = nameof(Ingredient), menuName = "Cooking/Core/Ingredient")]
    public class Ingredient : ScriptableObject, IGuid
    {
        #region Properties and Fields

        public string Guid
        {
            get => guid;
            set
            {
                guid = value;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        public string DisplayName => displayName;

        [SerializeField] private string guid = System.Guid.NewGuid().ToString();
        [SerializeField] private string displayName;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Guid))
            {
                Guid = System.Guid.NewGuid().ToString();
            }
        }

        #endregion
    }
}