
using UnityEngine;
using UseFoodComponent.NeedConstruction;

namespace UseMove
{
    public class StandFrontConstruction : NeedConstruction
    {
        [SerializeField] private Vector3 _offset;
        private void Update()
        {
            Stand();
        }
        private void Stand()
        {
            if (_constraction != null)
            {
                transform.position = _constraction.transform.position + _offset;
            }
        }
    }
}

