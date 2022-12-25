using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public GameManager GameManager;
    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(GameManager);
        }
    }
}
