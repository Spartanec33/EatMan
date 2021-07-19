using UnityEngine;

public class RoadRepeater : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    private Road road;
    private void Start()
    {
        road = FindObjectOfType<Road>();
        startPos = road.transform.position;
        repeatWidth = road.GetComponent<BoxCollider>().size.z / 2;
    }
    public void TryRepeat()
    {
        if (road.transform.position.z < startPos.z - repeatWidth)
            road.transform.position = startPos;
    }
}
