using System.Collections;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    [SerializeField] private float _stopDistance = 10;

    private SpeedComponent _speedCom;

    private void Start()
    {
        _speedCom = GetComponent<SpeedComponent>();
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
        else if(!Player.Died)
            _speedCom.UnStop();
    }
    private bool CheckForStop() => Distance.Value <= _stopDistance && Mover.NeedOneTimeStop;
}
