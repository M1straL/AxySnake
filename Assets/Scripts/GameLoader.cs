using DefaultNamespace;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private GameManager GameManager;
    private LevelManager LevelManager;

    private void Awake()
    {
        if (GameManager.instance == null) Instantiate(GameManager);

        if (LevelManager.instance == null) Instantiate(LevelManager);
    }
}