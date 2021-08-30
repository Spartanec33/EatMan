using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UseEvents;

namespace UseUIComponents
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _startUI;
        [SerializeField] private CanvasGroup _playModeUI;
        [SerializeField] private CanvasGroup _deadModeUI;
        [SerializeField] private CanvasGroup _blackoutCanvas;
        [SerializeField] private float _timeToTogle;
        [SerializeField] private float _timeToWaitAfterDie;
        [SerializeField] private float _timeToBlackout;

        private static WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        private static bool _wasBlackoutAfterDeath = false;
    
        private void OnEnable()
        {
            OnStartButtonClick.OnAction += ActivateSwitchStartToPlayMode;
            OnReloadButtonClick.OnAction += ActivateBlackOut;
            OnDie.OnAction += ActivateSwitchPlayModeToDeadMode;
        }
        private void OnDisable()
        {
            OnStartButtonClick.OnAction -= ActivateSwitchStartToPlayMode;
            OnReloadButtonClick.OnAction -= ActivateBlackOut;
            OnDie.OnAction -= ActivateSwitchPlayModeToDeadMode;
        }

        private void Start()
        {
            StartAntiBlackOut();
        }
        private void StartAntiBlackOut()
        {
            if (_wasBlackoutAfterDeath)
            {
                StartCoroutine(ChangeAlpha(_blackoutCanvas, 1, 0));
                StartCoroutine(ChangeAlpha(_blackoutCanvas, 0, _timeToBlackout));
            }
        }

        public IEnumerator ChangeAlpha(CanvasGroup canvasGroup, float finalAlpha, float time)
        {
            if (time!=0)
            {
                float speed = 1 / time;
                float progress = 0;
                var startAlpha = canvasGroup.alpha;
                while (progress <= 1)
                {
                    yield return _waitForFixedUpdate;
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, finalAlpha, progress);
                    progress += speed * Time.deltaTime;
                }
                if (progress >= 1)
                    canvasGroup.alpha = finalAlpha;
            }
            else
            {
                canvasGroup.alpha = finalAlpha;
            }
        }
        
        private IEnumerator TogleCanvas(CanvasGroup canvas, float time, bool activate)
        {
            if (canvas != null)
            {
                canvas.GetComponent<GraphicRaycaster>().enabled = activate;
                yield return StartCoroutine(ChangeAlpha(canvas, activate ? 1 : 0, time));  
            }
        }

        private IEnumerator SwitchStartToPlayMode(float time)
        {
            yield return TogleCanvas(_startUI, time, false);
            yield return TogleCanvas(_playModeUI, time, true);
            OnGameStarted.ActivateEvent();
        }
        private void ActivateSwitchStartToPlayMode()
        {
            StartCoroutine(SwitchStartToPlayMode(_timeToTogle));
        }

        private IEnumerator SwitchPlayModeToDeadMode(float time, float timeToWaitAfterDie)
        {
            yield return TogleCanvas(_playModeUI, time, false);
            yield return new WaitForSeconds(timeToWaitAfterDie);
            yield return TogleCanvas(_deadModeUI, time, true);
        }
        private void ActivateSwitchPlayModeToDeadMode()
        {
            StartCoroutine(SwitchPlayModeToDeadMode(_timeToTogle, _timeToWaitAfterDie));
        }

        private IEnumerator BlackOut()
        {
            yield return StartCoroutine(ChangeAlpha(_blackoutCanvas, 1, _timeToBlackout));
            OnReload.ActivateEvent();
            _wasBlackoutAfterDeath = true;
        }
        private void ActivateBlackOut()
        {
            StartCoroutine(BlackOut());
        }   
    }
}