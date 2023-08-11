using Cooking.Core.Record;
using System;
using System.Collections.Generic;

namespace Cooking.Core.Persistence
{
    [Serializable]
    public class ImageManagerDTO
    {
        public List<string> images = new List<string>();

        public ImageManagerDTO(ImageRecord imageRecord)
        {
            foreach (var imageEntry in imageRecord.Items)
            {
                images.Add(imageEntry.Key);
            }
        }
    }
}
