using UnityEngine;
using UseFoodComponent.Personal;
using UseFoodComponent.Logic;

namespace UseFoodComponent.ForInspector
{
    public class FoodListData : MonoBehaviour
    {
        [SerializeField] private Food[] _foods;
        public Food[] GetListData => _foods;
    }
}
