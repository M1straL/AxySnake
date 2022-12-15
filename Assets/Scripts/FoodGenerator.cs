using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    private float _time = 20f;
    private float _timer;

    [SerializeField] 
    private GameObject _gameObject;
    private Food _food;
    
    // Start is called before the first frame update
    void Start()
    {
        _timer = _time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.fixedDeltaTime;
        }
        else
        {
            Instantiate(_gameObject, transform.position, Quaternion.identity);
            _timer = _time;
        }
    }
}
