using UnityEngine;
using UseEvents;
using UnityEngine.SceneManagement;

namespace UseUIComponents
{
    public class ReloadScene : MonoBehaviour
    {
        private void OnEnable()
        {
            OnReload.OnAction += Reload;   
        }
        private void OnDisable()
        {
            OnReload.OnAction -= Reload;
        }
        private void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void Click()
        {
            OnReloadButtonClick.ActivateEvent();
        }
    }
}