using UnityEngine;
using UseEvents;


namespace UseUIComponents.Resolution
{
    public class CameraDynamicResolution : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultResolution = new Vector2(720, 1280);
        [Range(0f, 1f)] [SerializeField] private float _widthOrHeight = 0;

        private Camera _camera;
        private float _targetAspect;
        private float _initialFov;
        private float _horizontalFov;

        private void OnEnable()
        {
            OnChangeResolution.OnAction += ChangeFov;
        }
        private void OnDisable()
        {
            OnChangeResolution.OnAction -= ChangeFov;
        }

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _targetAspect = _defaultResolution.x / _defaultResolution.y;
            _initialFov = _camera.fieldOfView;
            _horizontalFov = CalcVerticalFov(_initialFov, 1 / _targetAspect);
        }

        private void ChangeFov()
        {
            var startTargetTexture = _camera.targetTexture;
            if (startTargetTexture != null)
                _camera.targetTexture = null;

            float constantWidthFov = CalcVerticalFov(_horizontalFov, _camera.aspect);
            _camera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, _widthOrHeight);

            _camera.targetTexture = startTargetTexture;
        }

        private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

            float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

            return vFovInRads * Mathf.Rad2Deg;
        }
    }
}

