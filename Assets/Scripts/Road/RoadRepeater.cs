using UnityEngine;

public class RoadRepeater : MonoBehaviour
{
    private Vector3 _startPos;
    private float _repeatWidth;
    private Road _road;

    private void Start()
    {
        _road = FindObjectOfType<Road>();
        _startPos = _road.transform.position;
        _repeatWidth = _road.GetComponent<BoxCollider>().size.z / 2;
    }
    private void TryRepeat()
    {
        if (_road.transform.position.z < _startPos.z - _repeatWidth)
            _road.transform.position = _startPos;
    }
    private void FixedUpdate()
    {
        TryRepeat();
    }
}
