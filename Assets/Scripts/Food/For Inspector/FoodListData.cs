using UnityEngine;


public class FoodListData : MonoBehaviour
{
    [SerializeField] private Food[] _foods;

    public Food[] GetListData => _foods;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            FoodSpawner.Spawn();

        if (Input.GetKeyDown(KeyCode.V))
            FoodSpawner.Delete();

    }

}
