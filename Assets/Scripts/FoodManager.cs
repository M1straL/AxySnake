using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UniRx;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 5f;

    [SerializeField] private float _spawnRadius = 10;

    [SerializeField] private int _maxCountOnLevel = 25;

    private int _amountOnStart;

    private Ctx _ctx;
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
            SpawnFood(_foodPrefabs.First());
            _timer = _spawnTime;
        }

        //Попробовать использовать TimeSpan
        //DateTime.Now
    }

    public void SetCtx(Ctx ctx)
    {
        _ctx = ctx;
    }

    private Food SpawnFood(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.Log("Food Prefab is null");
            return null;
        }

        var foodObj = _pool.Get(prefab);
        foodObj.tag = "Food";

        _objectsInScene.Add(foodObj);

        foodObj.SetCtx(new Food.Ctx
        {
            Score = _ctx.Score,
            Died = _ctx.FoodDied.ObserveAdd();
        });

        _ctx.FoodDied.Subscribe(unit =>
        {
            _pool.Push(foodObj);
            _objectsInScene.Remove(foodObj);
        }).AddTo(this);

        var go = foodObj.gameObject;
        go.transform.position = GetRandomPosition() + transform.position;

        return foodObj;
        //TODO добавить анимацию спавна
    }

    public void Init(List<PoolInitData> levelDataFoodDatas)
    {
        _pool = new Pool<Food>(levelDataFoodDatas);
        _foodPrefabs = _pool.GetPrefabs();
        _maxCountOnLevel = levelDataFoodDatas.First().MaxCountOnLevel;
        _amountOnStart = levelDataFoodDatas.First().Count;
        _objectsInScene = new List<Food>();

        PrepareFood(_foodPrefabs.First()); //TODO Сделать создание для каждого типа префаба.
    }

    public void PrepareFood(GameObject prefab)
    {
        for (var i = 0; i < _amountOnStart; i++) SpawnFood(prefab);
    }

    private Vector3 GetRandomPosition()
    {
        var positions = _objectsInScene.Select(pos => pos.transform.position);

        var maxPos = Vector3.zero;
        foreach (var position in positions)
            if (position.magnitude > maxPos.magnitude)
                maxPos = position;

        //TODO дописать логику рассчета расстояния

        var delta = Vector3.zero; //Дельта расстояния между любым существующим и новым.
        var meshBounds = Vector3.zero; //Расстояние от центра до границ меша объекта

        var randomX = Random.Range(_spawnRadius * -1, _spawnRadius);
        var randomZ = Random.Range(_spawnRadius * -1, _spawnRadius);

        return new Vector3(randomX, 5F, randomZ);
    }

    public struct Ctx
    {
        public ReactiveCommand<int> Score;
        public ReactiveCollection<bool> FoodDied; //
    }
}