using Celeste.Events;
using Celeste.UI;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking.Core.UI
{
    [AddComponentMenu("Cooking/Core/UI/Web Cam Capture Popup Controller")]
    public class WebCamCaptureFullScreenPopupController : MonoBehaviour, IPopupController
    {
        #region Properties and Fields

        [SerializeField] private RawImage webCameraOutput;
        [SerializeField] private AspectRatioFitter fitter;

        private WebCamTexture webCamTexture;

        #endregion

        #region IPopupController

        public void OnShow(IPopupArgs args)
        {
            if (webCamTexture == null)
            {
                webCamTexture = new WebCamTexture();
            }

            webCamTexture.Play();
        }

        public void OnHide()
        {
            webCamTexture.Stop();
        }

        public void OnConfirmPressed()
        {
            StartCoroutine(TakePhoto());
        }

        public void OnClosePressed()
        {
        }

        #endregion

        private IEnumerator TakePhoto()
        {
            yield return new WaitForEndOfFrame();

            Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
            photo.SetPixels(webCamTexture.GetPixels());
            photo.Apply();

            byte[] bytes = photo.EncodeToPNG();
            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "photo.png"), bytes);
        }

        #region Unity Methods

        private void Update()
        {
            if (webCamTexture != null)
            {
                webCameraOutput.texture = webCamTexture;

                float ratio = webCamTexture.width / (float)webCamTexture.height;
                fitter.aspectRatio = ratio;

                float scaleY = webCamTexture.videoVerticallyMirrored ? -1f : 1f;
                webCameraOutput.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

                int orient = -webCamTexture.videoRotationAngle;
                webCameraOutput.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
            }
        }

        #endregion
    }
}
