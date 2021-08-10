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
    private void LateUpdate()
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
        if ((Distance.Value <= _stopDistance) && Mover.NeedOneTimeStop)
        {
            Stop1Event.ActivateEvent();
            return true;
        }
        else if(Player.IsDied || Player.IsPuke)
        {
            return true;
        }
        return ((Distance.Value <= _stopDistance) && Mover.NeedOneTimeStop) || Player.IsDied;
    }

    public float GetDelta()
    {        
        return _stopDistance - Distance.Value;
    }

}
