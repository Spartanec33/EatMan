using UnityEngine;
using UseRoadComponent;

namespace UseMove
{
    [RequireComponent(typeof(Distance))]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(SpeedComponent))]
    [RequireComponent(typeof(RoadRepeater))]
    [RequireComponent(typeof(Stopper))]
    public class MoveManager : MonoBehaviour
    {

    }
}
