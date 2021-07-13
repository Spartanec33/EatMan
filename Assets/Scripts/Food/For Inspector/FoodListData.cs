using System.Collections;
using UnityEngine;


public class FoodListData : MonoBehaviour
{


    [SerializeField] private Food[] foods;
    public Food[] GetListData => foods;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            FoodSpawner.Spawn();
            Debug.Log("тф:"+FoodComparer.TargetFood);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            FoodSpawner.Delete();
        }
    }

}
