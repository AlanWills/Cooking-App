using Celeste.Events;
using Celeste.UI;
using Celeste.UI.Popups;
using Cooking.Core.Record;
using Cooking.Core.Runtime;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking.Core.UI
{
    public class WebCamCapturePopupArgs : IPopupArgs
    {
        public RecipeStepRuntime CurrentStepRuntime;

        public WebCamCapturePopupArgs(RecipeStepRuntime recipeStepRuntime)
        {
            CurrentStepRuntime = recipeStepRuntime;
        }
    }

    [AddComponentMenu("Cooking/Core/UI/Web Cam Capture Popup Controller")]
    public class WebCamCaptureFullScreenPopupController : MonoBehaviour, IPopupController
    {
        #region Properties and Fields

        [Header("UI Elements")]
        [SerializeField] private Popup popup;
        [SerializeField] private RawImage webCameraOutput;
        [SerializeField] private AspectRatioFitter fitter;

        [Header("Data")]
        [SerializeField] private ImageRecord imageRecord;

        private WebCamTexture webCamTexture;
        private WebCamCapturePopupArgs popupArgs;

        #endregion

        #region IPopupController

        public void OnShow(IPopupArgs args)
        {
            popupArgs = args as WebCamCapturePopupArgs;

            if (webCamTexture == null)
            {
                webCamTexture = new WebCamTexture();
            }

            webCamTexture.Play();
        }

        public void OnHide()
        {
            webCamTexture.Stop();
            popupArgs = null;
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

            Sprite newImage = imageRecord.SaveImage(photo);
            popupArgs.CurrentStepRuntime.AddImage(newImage);

            popup.Hide();
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
