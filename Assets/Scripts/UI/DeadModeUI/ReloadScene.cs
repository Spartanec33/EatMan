using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UseUIComponents
{
    public class ReloadScene : MonoBehaviour
    {
        
        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}