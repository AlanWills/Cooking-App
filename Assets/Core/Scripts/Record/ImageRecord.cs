using Celeste.Objects;
using System;
using System.IO;
using UnityEngine;

namespace Cooking.Core.Record
{
    [CreateAssetMenu(fileName = nameof(ImageRecord), menuName = "Cooking/Core/Image Record")]
    public class ImageRecord : DictionaryScriptableObject<string, Sprite>
    {
        #region Properties and Fields

        [SerializeField] private Celeste.Events.Event saveEvent;

        #endregion

        public Sprite SaveImage(Texture2D texture)
        {
            byte[] bytes = texture.EncodeToPNG();
            string guid = Guid.NewGuid().ToString();
            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, $"{guid}.png"), bytes);
            
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
            sprite.name = guid;
            AddItem(guid, sprite);
            saveEvent.Invoke();

            return sprite;
        }

        public Sprite LoadImage(string imageId)
        {
            string imagePath = Path.Combine(Path.Combine(Application.persistentDataPath, $"{imageId}.png"));
            
            if (!File.Exists(imagePath))
            {
                return null;
            }

            byte[] bytes = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(1, 1);
            
            if (!texture.LoadImage(bytes))
            {
                return null;
            }

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
            sprite.name = imageId;
            AddItem(imageId, sprite);

            return sprite;
        }
    }
}
