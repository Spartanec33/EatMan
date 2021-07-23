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
            if (FoodOnClick.IsCoroutineActive == true)
                Mover.WasOneTimeStop = true;
        }
    }
    private bool CheckForStop()
    {
        return DistanceFinder.Find() <= _stopDistance && Mover.WasOneTimeStop == false && Mover.NeedOneTimeStop;
    }
}
