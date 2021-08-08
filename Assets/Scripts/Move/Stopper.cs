using System.Collections;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    [SerializeField] private float _stopDistance = 10;

    private SpeedComponent _speedCom;
    private Player _player;

    private void Start()
    {
        _speedCom = GetComponent<SpeedComponent>();
        _player = FindObjectOfType<Player>();
    }
    private void FixedUpdate()
    {
        TryStop();
    }
    private void TryStop()
    {
        if (CheckForStop())
        {
            _speedCom.Stop();
        }
        else
            _speedCom.UnStop();
    }
    private bool CheckForStop()
    {
        return ((Distance.Value <= _stopDistance) && Mover.NeedOneTimeStop) || Player.Died;
    }
}
