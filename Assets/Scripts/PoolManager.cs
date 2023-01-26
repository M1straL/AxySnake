using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager instance;

        private List<Pool<Food>> _pools;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance == this) Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            InitializeManager();
        }

        private void InitializeManager()
        {
        }

        public Pool<T> GetPool<T>(List<PoolInitData> poolInitDatas) where T : Component, IPoolObject
        {
            var pool = new Pool<T>(poolInitDatas);
            return pool;
        }

        public Pool<T> PrepareObjects<T>(List<PoolInitData> poolInitDatas) where T : Component, IPoolObject
        {
            return new(poolInitDatas);
        }
    }
}