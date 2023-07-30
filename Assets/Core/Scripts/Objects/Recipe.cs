using Celeste.DataStructures;
using Celeste.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Core.Objects
{
    [CreateAssetMenu(fileName = nameof(Recipe), menuName = "Cooking/Core/Recipe")]
    public class Recipe : ScriptableObject, IGuid
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
        public Sprite Thumbnail => thumbnail;
        public IReadOnlyList<RecipeStep> Steps => steps;

        [SerializeField] private string guid = System.Guid.NewGuid().ToString();
        [SerializeField] private string displayName;
        [SerializeField] private Sprite thumbnail;
        [SerializeField] private List<RecipeStep> steps = new List<RecipeStep>();

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

        public void AddEmptyStep(Sprite sprite)
        {
            steps.Add(new RecipeStep(sprite));
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}