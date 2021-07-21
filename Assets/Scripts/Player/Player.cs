using UnityEngine;

public class Player : MonoBehaviour
{
    private SpeedChanger _speedChanger;
    private void Start()
    {
        _speedChanger = GameObject.FindObjectOfType<SpeedChanger>();
    }
    private void OnMouseDown()
    {
        _speedChanger.AddSpeedPerClick();
    }
}
