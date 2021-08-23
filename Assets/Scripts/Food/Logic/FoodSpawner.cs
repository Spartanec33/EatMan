using UnityEngine;
using UseEvents;
using UseFoodComponent.ForInspector.Service;
using UseFoodComponent.Personal;
using UsePlayerComponents;

namespace UseFoodComponent.Logic
{
    public class FoodSpawner: MonoBehaviour
    {
        [SerializeField] private FoodSpawnData _data;
        private Player _player;
        private static GameObject _construction;
        private FoodGetter _foodGetter;
        private Food[] _foods;


        public static GameObject Construction => _construction;
        private void OnEnable()
        {
            OnSpawnConstruction.OnAction += Spawn;
            OnDeleteConstruction.OnAction += Delete;
        }
        private void OnDisable()
        {
            OnSpawnConstruction.OnAction -= Spawn;
            OnDeleteConstruction.OnAction -= Delete;
        }
        private void Start()
        {
            _player = FindObjectOfType<Player>();
            _foodGetter = GetComponent<FoodGetter>();
            _foods = _foodGetter.GetFoods();
        }
        private void Spawn()
        {
            if (_construction == null)
            {
                _foodGetter.ChooseFood();

                _construction = CreateConstuction();

                int placeForTargetFood = Random.Range(0, _data.NumberOfPieces);
                Quaternion rotation = _construction.transform.rotation;
                Vector3 position = _construction.transform.position;
                DirectlyGenerate(placeForTargetFood, rotation, position);
                OnChangeConstruction.ActivateEvent();
            }
        }
        private void Delete()
        {
            if (_construction != null)
            {
                Destroy(_construction);
                _construction = null;
                OnChangeConstruction.ActivateEvent();
            }


        }

        private GameObject CreateConstuction()
        {
            var construction = new GameObject();

            var z = _player.transform.position.z + Random.Range(_data.ZMinPosition, _data.ZMaxPosition);
            var pos = new Vector3(_data.ConstructionPosition.x, _data.ConstructionPosition.y, z);
            construction.transform.SetPositionAndRotation(pos, _data.ConstructionRotation);

            return construction;
        }
        private void DirectlyGenerate(int placeForTargetFood, Quaternion rotation, Vector3 position)
        {
            for (int i = 0; i < _data.NumberOfPieces; i++)
            {
                float placeX = position.x + _data.Offset * i;
                Vector3 place = new Vector3(placeX, position.y, position.z);


                if (i != placeForTargetFood)
                {
                    var randomFood = _foods[Random.Range(0, _foods.Length)];
                    SpawnOneFood(randomFood);
                }
                else
                    SpawnOneFood(FoodGetter.TargetFood);

                void SpawnOneFood(Food spawningFood)
                {
                    var food = Instantiate(spawningFood, place, rotation);
                    food.Init(_data.AnimController, _data.AudioSourceEat);
                    food.transform.SetParent(_construction.transform);
                }
            }
        }

    }
}

