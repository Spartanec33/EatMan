using UnityEngine;


public class FoodListData : MonoBehaviour
{


    [SerializeField] private Food[] _foods;
    public Food[] GetListData => _foods;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            FoodSpawner.Spawn();
            for (int i = 0; i < FoodComparer.TargetProperties.Length; i++)
                Debug.Log(FoodComparer.TargetProperties[i]);
            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            FoodSpawner.Delete();
        }
    }

}
