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
    }
}
