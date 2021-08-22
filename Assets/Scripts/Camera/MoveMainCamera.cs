using System.Collections;
using UseEvents;
using UnityEngine;

namespace UseCameraComponents
{
    public class MoveMainCamera : MonoBehaviour
    {

        [SerializeField] private float _speed;

        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        private void OnEnable()
        {
            OnStartButtonClick.OnAction += ActivateChangeCameraTransform;
        }
        private void OnDisable()
        {
            OnStartButtonClick.OnAction -= ActivateChangeCameraTransform;
        }
        
        private IEnumerator ChangeCameraTransform()
        {
            float progress = 0;
            Vector3 startPos = transform.position;
            Quaternion startRotation = transform.rotation;

            while (progress < 1)
            {
                yield return _waitForFixedUpdate;
                progress += _speed;
                transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0,0,0), progress);
                transform.position = Vector3.Lerp(startPos, Vector3.zero, progress);
            }
            if (progress > 1)
            {
                transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0, 0, 0), 1);
                transform.position = Vector3.Lerp(startPos, Vector3.zero, 1);
            }
        }
        private void ActivateChangeCameraTransform()
        {
            StartCoroutine(ChangeCameraTransform());
        }
    }
}