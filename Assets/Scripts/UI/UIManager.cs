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
        [SerializeField] private float _time;

        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        private void OnEnable()
        {
            OnStartButtonClick.OnAction += ActivateChangeActiveCanvas;    
        }
        private void OnDisable()
        {
            OnStartButtonClick.OnAction -= ActivateChangeActiveCanvas;
        }
        private IEnumerator ChangeAlpha(CanvasGroup canvasGroup,float finalAlpha, float time)
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
        
        private IEnumerator ChangeActiveCanvas()
        {
            yield return StartCoroutine(ChangeAlpha(_startUI, 0, _time));
            _startUI.gameObject.SetActive(false);
            _playModeUI.GetComponent<GraphicRaycaster>().enabled = true;
            yield return StartCoroutine(ChangeAlpha(_playModeUI, 1, _time));
            OnGameStarted.ActivateEvent();
        }
        private void ActivateChangeActiveCanvas()
        {
            StartCoroutine(ChangeActiveCanvas());
        }
    }
}