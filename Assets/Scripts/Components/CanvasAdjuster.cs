using UnityEngine;
using UnityEngine.UI;

namespace Component
{
    [ExecuteInEditMode]
    public class CanvasAdjuster : MonoBehaviour
    {
        public enum CanvasScaleMatchMode
        {
            SCREEN_BOTH,
            SCREEN_WIDTH,
            SCREEN_HEIGHT,
        }

        public float screenWidth = 1280f;
        public float screenHeight = 720f;

        [SerializeField] private CanvasScaleMatchMode scaleMatchMode_ = CanvasScaleMatchMode.SCREEN_BOTH;

        private void Awake()
        {
            CanvasScaler scaler = GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(screenWidth, screenHeight);

            Rect rect = GetComponent<RectTransform>().rect;
            float screenRatio = scaler.referenceResolution.y / scaler.referenceResolution.x;

            switch(scaleMatchMode_)
            {
                case CanvasScaleMatchMode.SCREEN_BOTH:
                    scaler.matchWidthOrHeight = 0.5f;
                    break;
                case CanvasScaleMatchMode.SCREEN_WIDTH:
                    scaler.matchWidthOrHeight = ((rect.height / rect.width) < screenRatio) ? 0 : 1;
                    break;
                case CanvasScaleMatchMode.SCREEN_HEIGHT:
                    scaler.matchWidthOrHeight = ((rect.height / rect.width) < screenRatio) ? 1 : 0;
                    break;
            }
        }
    }
}
