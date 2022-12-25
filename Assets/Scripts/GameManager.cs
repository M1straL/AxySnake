using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int _score;
    private void Awake () {

        if (instance == null) {
            instance = this;
        } else if(instance == this){
            Destroy(gameObject); 
        }
        
        DontDestroyOnLoad(gameObject);
        
        InitializeManager();
    }

    private void InitializeManager()
    {
        _score = 0;
    }

    public void OnFoodDieEvent(Food food, int score)
    {
        _score += score;
    }
}
