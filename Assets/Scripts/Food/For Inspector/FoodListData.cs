using UnityEngine;
using UseFoodComponent.Personal;
using UseFoodComponent.Logic;

namespace UseFoodComponent.ForInspector
{
    public class FoodListData : MonoBehaviour
    {
        [SerializeField] private Food[] _foods;
        [SerializeField] private int _maxNumberOfTargetProperties;
        public Food[] GetListData => _foods;
        public int MaxNumberOfTargetProperties => _maxNumberOfTargetProperties;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                FoodSpawner.Spawn();

            if (Input.GetKeyDown(KeyCode.V))
                FoodSpawner.Delete();
        }
    }
}
