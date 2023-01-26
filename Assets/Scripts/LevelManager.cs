using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        [SerializeField] private LevelsConfig _levelsConfig;

        private FoodManager _foodManager;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance == this) Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            _foodManager = new FoodManager();
        }

        private void Start()
        {
        }

        private LevelsConfig.LevelData GetLevelData(int level)
        {
            return _levelsConfig._levelDatas.FirstOrDefault(data => data._level == level);
        }


        public void InitializeLevel(int level)
        {
            var levelData = GetLevelData(level);

            _foodManager.Init(levelData.FoodDatas);
        }
    }
}