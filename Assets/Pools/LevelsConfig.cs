using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/LevelsConfig", order = 1)]
    public class LevelsConfig : ScriptableObject
    {
        public List<LevelData> _levelDatas;

        [Serializable]
        public class LevelData
        {
            public int _level;
            public List<PoolInitData> EnemyDatas;
            public List<PoolInitData> FoodDatas;
            public List<PoolInitData> CollectableDatas;
        }
    }
}