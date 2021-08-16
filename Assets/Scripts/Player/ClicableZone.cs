using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UseMove;

namespace UsePlayerComponents
{
    public class ClicableZone : MonoBehaviour
    {
        private SpeedComponent _speedCom;
        private void Start()
        {
            _speedCom = GameObject.FindObjectOfType<SpeedComponent>();
        }
        private void OnMouseDown()
        {
            _speedCom.AddSpeedPerClick();
        }
    }
}
