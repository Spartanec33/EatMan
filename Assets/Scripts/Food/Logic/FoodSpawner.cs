using UnityEngine;

public static class FoodSpawner 
{
    private static InspectorFoodSpawnData _inspectordata = GameObject.FindObjectOfType<InspectorFoodSpawnData>();
    private static FoodSpawnData _data = _inspectordata.Data;
    private static Player _player = GameObject.FindObjectOfType<Player>();
    private static FoodOnClickController _foodOnClick = GameObject.FindObjectOfType<FoodOnClickController>();
    private static RuntimeAnimatorController _animController = _data.AnimController;
    private static GameObject _construction;
    private static readonly Food[] _foods = FoodGetter.GetFoods();

    public static GameObject Construction => _construction;
    
    public static void Spawn()
    {
        if (_construction == null)
        {
            ChooseFood();

            _construction = CreateConstuction();

            int placeForTargetFood = Random.Range(0, _data.NumberOfPieces);
            Quaternion rotation = _construction.transform.rotation;
            Vector3 position = _construction.transform.position;
            DirectlyGenerate(placeForTargetFood, rotation, position);
            ChangeConstructionEvent.ActivateEvent();

            for (int i = 0; i < FoodComparer.TargetProperties.Length; i++)
                Debug.Log(FoodComparer.TargetProperties[i]);
        }
    }
    public static void Delete()
    {
        if (_construction != null)
        {
            GameObject.Destroy(_construction);
            _construction = null;
            ChangeConstructionEvent.ActivateEvent();
        }
            
            
    }
    private static void ChooseFood()
    {
        FoodComparer.TargetFood = FoodGetter.GetRandomFood();
        FoodComparer.TargetProperties = FoodGetter.GetRandomProperties(FoodComparer.TargetFood);
    }
    private static GameObject CreateConstuction()
    {
        var construction = new GameObject();

        var z = _player.transform.position.z + Random.Range(_data.ZMinPosition, _data.ZMaxPosition);
        var pos = new Vector3(_data.ConstructionPosition.x, _data.ConstructionPosition.y, z);
        construction.transform.SetPositionAndRotation(pos, _data.ConstructionRotation);

        return construction;
    }
    private static void DirectlyGenerate(int placeForTargetFood, Quaternion rotation, Vector3 position)
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
                SpawnOneFood(FoodComparer.TargetFood);

            void SpawnOneFood(Food spawningFood)
            {
                var food = GameObject.Instantiate(spawningFood, place, rotation);
                food.Init(_animController);
                food.transform.SetParent(_construction.transform);
            }
        }
    }

}

