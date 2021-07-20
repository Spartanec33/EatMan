using UnityEngine;

public static class FoodSpawner 
{
    private static FoodSpawnData _data;
    private static Player _player = GameObject.FindObjectOfType<Player>();
    private static FoodOnClick _foodOnClick = GameObject.FindObjectOfType<FoodOnClick>();
    private static readonly InspectorFoodSpawnData _inspectorData = GameObject.FindObjectOfType<InspectorFoodSpawnData>();
    public static GameObject _construction;

    private static readonly Food[] _foods = FoodGetter.GetFoods();


    //выбрать правильную еду и получить ее рандомные свойства
    private static void ChooseFood()
    {
        FoodComparer.TargetFood = FoodGetter.GetRandomFood();
        FoodComparer.TargetProperties = FoodGetter.GetRandomProperties(FoodComparer.TargetFood);
    }
    public static void Spawn()
    {
        if (_construction == null)
        {

            ChooseFood();
            int placeForTargetFood = Random.Range(0, _data.NumberOfPieces);

            _construction=CreateConstuction();

            Quaternion rotation = _construction.transform.rotation;
            Vector3 position = _construction.transform.position;

            for (int i = 0; i < _data.NumberOfPieces; i++)
            {
                float placeX = position.x+_data.Offset*i;
                Vector3 place = new Vector3(placeX, position.y, position.z);

                void Spawn(Food spawningFood)
                {
                    var food = GameObject.Instantiate(spawningFood, place, rotation);
                    food.Init(_foodOnClick);
                    food.transform.SetParent(_construction.transform);
                }

                if (i != placeForTargetFood)
                {
                    var randomFood = _foods[Random.Range(0, _foods.Length)];
                    Spawn(randomFood);
                }
                else
                    Spawn(FoodComparer.TargetFood);
            }
        }
    }
    public static void Delete()
    {
        if (_construction != null)
        {
            GameObject.Destroy(_construction);
            _construction = null;
        }
            
            
    }
    private static GameObject CreateConstuction()
    {
        var construction = new GameObject();

        _data = _inspectorData.Data;

        var z = _player.transform.position.z + Random.Range(_data.ZMinPosition, _data.ZMaxPosition);
        var pos = new Vector3(_data.ConstructionPosition.x, _data.ConstructionPosition.y, z);
        construction.transform.SetPositionAndRotation(pos, _data.ConstructionRotation);

        return construction;
    }
}
