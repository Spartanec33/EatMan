using UnityEngine;

public class Player : MonoBehaviour
{
    private Mover mover;
    private void Start()
    {
        mover = GameObject.FindObjectOfType<Mover>();
    }
    private void OnMouseDown()
    {
        mover.AddSpeedPerClick();
    }
}
