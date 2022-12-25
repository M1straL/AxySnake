using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FoodGenerator : MonoBehaviour
{
    private float _time = 20f;
    private float _timer;
    private int _minRadius = 1;
    private int _maxRadius = 10;
    
    private Vector3 foodPosition;

    [SerializeField] 
    private GameObject[] _foodPrefabs;

    private ObjectPool<Food> _foodPool;

    void Start()
    {
        _timer = _time;
    }
    
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.fixedDeltaTime;
        }
        else
        {
            SpawnFood();
            _timer = _time;
        }
    }

    private void SpawnFood()
    {
        //TODO описать работу с пулом.

        var randomIdx = Random.Range(0, _foodPrefabs.Length - 1);
        var foodPrefab = _foodPrefabs[randomIdx];
        
        if (foodPrefab == null)
        {
            return;
        }
        
        var foodObj = Instantiate(_foodPrefabs[randomIdx], foodPosition, Quaternion.identity);
        var food = foodObj.GetComponent<Food>();
        foodObj.tag = "Food";

        food.DieEvent = GameManager.instance.OnFoodDieEvent;
    }
    
    private void 

    private Vector3 CalculateFoodPosition()
    {
        //TODO рассчитать нормальный радиус
        return Vector3.zero; 
    }
}
