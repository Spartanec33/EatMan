using UnityEngine;

public class Player : MonoBehaviour
{
    private Mover _mover;
    private void Start()
    {
        _mover = GameObject.FindObjectOfType<Mover>();
    }
    private void OnMouseDown()
    {
        _mover.AddSpeedPerClick();
    }
}
