using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 20f;

    [SerializeField] private float _spawnRadius = 10;

    [SerializeField] private int _maxCountOnLevel = 25;

    private int _amountOnStart;
    private int _currentCountOnLevel;

    private List<GameObject> _foodPrefabs;
    private List<Food> _objectsInScene;
    private Pool<Food> _pool;

    private float _timer;
    private Vector3 foodPosition;

    private void Start()
    {
        _timer = _spawnTime;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else if (_objectsInScene.Count < _maxCountOnLevel)
        {
            SpawnFood();
            _timer = _spawnTime;
        }

        //Попробовать использовать TimeSpan
        //DateTime.Now
    }

    private void SpawnFood()
    {
        var randomIdx = Random.Range(0, _foodPrefabs.Count - 1);
        var foodPrefab = _foodPrefabs[randomIdx];

        if (foodPrefab == null) return;

        var foodObj = _pool.Get(foodPrefab);
        foodObj.tag = "Food";

        _objectsInScene.Add(foodObj);

        foodObj.DieEvent = GameManager.instance.OnFoodDieEvent;
        foodObj.DieEvent = (_, _) =>
        {
            _pool.Push(foodObj);
            _objectsInScene.Remove(foodObj);
        };

        var go = foodObj.gameObject;
        go.transform.position += GetRandomPosition(); //TODO добавить анимацию
        //Animation clip
    }

    public void Init(List<PoolInitData> levelDataFoodDatas)
    {
        _pool = new Pool<Food>(levelDataFoodDatas);
        _foodPrefabs = _pool.GetPrefabs();
        _maxCountOnLevel = levelDataFoodDatas.First().MaxCountOnLevel;
        _amountOnStart = levelDataFoodDatas.First().Count;

        PrepareFood(_foodPrefabs.First()); //TODO Сделать создание для каждого типа префаба.
    }

    public void PrepareFood(GameObject prefab)
    {
        for (var i = 0; i < _amountOnStart; i++)
        {
            var obj = _pool.Get(prefab);
            _objectsInScene.Add(obj);
        }
    }


    private Vector3 GetRandomPosition()
    {
        var randomX = Random.Range(_spawnRadius * -1, _spawnRadius);
        var randomZ = Random.Range(_spawnRadius * -1, _spawnRadius);

        return new Vector3(randomX, 0, randomZ);
    }
}