using UnityEngine;
using UnityEngine.UI;

namespace Component
{
    [ExecuteInEditMode]
    public class CanvasAdjuster : MonoBehaviour
    {
        public float screenWidth = 1280f;
        public float screenHeight = 720f;
        public bool scaleMatchWidth = false;
        public bool scaleMatchHeight = false;

        private void Awake()
        {
            CanvasScaler scaler = GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(screenWidth, screenHeight);

            Rect rect = GetComponent<RectTransform>().rect;
            float screenRatio = scaler.referenceResolution.y / scaler.referenceResolution.x;

            if (scaleMatchWidth && scaleMatchHeight)
            {
                scaler.matchWidthOrHeight = 0.5f;
            }
            else if (scaleMatchWidth) 
            {
                scaler.matchWidthOrHeight = ((rect.height / rect.width) < screenRatio) ? 0 : 1;
            }
            else if (scaleMatchHeight)
            {
                scaler.matchWidthOrHeight = ((rect.height / rect.width) < screenRatio) ? 1 : 0;
            }
        }
    }
}
