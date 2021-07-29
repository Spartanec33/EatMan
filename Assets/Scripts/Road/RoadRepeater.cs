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
        _repeatWidth = _road.GetComponent<BoxCollider>().size.z ;
    }
    private void TryRepeat()
    {
        if (_road.transform.position.z < _startPos.z - _repeatWidth)
        {
            float inaccuracy = _startPos.z - _repeatWidth - _road.transform.position.z;
            Vector3 pos = new Vector3(_startPos.x, _startPos.y, _startPos.z - inaccuracy);
            _road.transform.position = pos;
        }
    }
    private void FixedUpdate()
    {
        TryRepeat();
    }
}
