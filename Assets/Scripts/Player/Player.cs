using UnityEngine;

public class Player : MonoBehaviour
{
    private SpeedComponent _speedCom;
    private void Start()
    {
        _speedCom = GameObject.FindObjectOfType<SpeedComponent>();
    }
    private void OnMouseDown()
    {
        _speedCom.AddSpeedPerClick();
    }
}
