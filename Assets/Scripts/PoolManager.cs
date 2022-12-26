using UnityEngine;

namespace DefaultNamespace
{
    public class PoolManager : MonoBehaviour
    {
        public struct PoolData
        {
            public string name;
            public PoolObject prefab;
            public int count;
            public ObjectPooling pool;
        }
        
        public static PoolManager instance = null;
        
        private static PoolData[] _pools;
        private static GameObject _parentObject;
        private void Awake () {

            if (instance == null) {
                instance = this;
            } else if(instance == this){
                Destroy(gameObject); 
            }
        
            DontDestroyOnLoad(gameObject);
        
            InitializeManager(_pools);
        }

        private void InitializeManager(PoolData[] poolDatas)
        {
            _pools = poolDatas;
            _parentObject = new GameObject();
            _parentObject.name = "Pools";
            for (int i = 0; i < _pools.Length; i++)
            {
                if (_pools[i].prefab != null)
                {
                    _pools[i].pool = new ObjectPooling();
                    _pools[i].pool.Initialize(_pools[i].count, _pools[i].prefab, _parentObject.transform);
                }
            }
        }

        public GameObject GetObject (string name, Vector3 position, Quaternion rotation) {
            GameObject result = null;
            if (_pools != null) {
                for (int i = 0; i < _pools.Length; i++) {
                    if (string.Compare (_pools [i].name, name) == 0) { 
                        result = _pools[i].pool.GetObject ().gameObject; 
                        result.transform.position = position;
                        result.transform.rotation = rotation; 
                        result.SetActive (true); 
                        return result;
                    }
                }
            } 
            return result;
        }
    }
}