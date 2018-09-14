using UnityEngine;

namespace Component
{
    [ExecuteInEditMode]
    public class CameraAdjuster : MonoBehaviour
    {
        public enum CameraScaleMatchMode
        {
            SCREEN_WIDTH,
            SCREEN_HEIGHT,
        }

        public float activeZoneRange = 8f;

        [SerializeField] private CameraScaleMatchMode scaleMatchMode_ = CameraScaleMatchMode.SCREEN_WIDTH;

        private void Awake()
        {
            AdjustActiveZone();
        }

        public void AdjustActiveZone()
        {
            float screenAspect = Camera.main.aspect;

            switch (scaleMatchMode_)
            {
                case CameraScaleMatchMode.SCREEN_WIDTH:
                    Camera.main.orthographicSize = (1f / screenAspect) * activeZoneRange / 2f;
                    break;
                case CameraScaleMatchMode.SCREEN_HEIGHT:
                    Camera.main.orthographicSize = activeZoneRange / 2f;
                    break;
            }
        }
    }
}

