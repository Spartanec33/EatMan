using System.Collections;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    [SerializeField] private float _stopDistance = 10;

    private Mover _mover;

    private void Start()
    {
        _mover = GetComponent<Mover>();
    }
    private void FixedUpdate()
    {
        TryStop();
    }
    private void TryStop()
    {
        if (CheckForStop())
        {
            _mover.Speed = 0;
            if (FoodOnClick.IsCoroutineActive == true)
                Mover.WasOneTimeStop = true;
        }
    }
    private bool CheckForStop()
    {
        return DistanceFinder.Find() <= _stopDistance && Mover.WasOneTimeStop == false && Mover.NeedOneTimeStop;
    }
}
