using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("MenuScene");
        Init();
    }

    private void Init()
    {
        var root = new Root();
    }
}