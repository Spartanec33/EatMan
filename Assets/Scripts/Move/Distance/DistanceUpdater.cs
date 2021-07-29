using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceUpdater : MonoBehaviour
{
    private void FixedUpdate()
    {
        Distance.DistanceUpdate();
    }
}
