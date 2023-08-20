using CelesteEditor.Objects;
using Cooking.Core.Record;
using UnityEditor;
using UnityEngine;

namespace CookingEditor.Core.Record
{
    [CustomEditor(typeof(ImageRecord))]
    public class ImageRecordEditor : DictionaryScriptableObjectEditor<string, Sprite>
    {
        protected override string GetKey(Sprite item)
        {
            return item.name;
        }
    }
}
