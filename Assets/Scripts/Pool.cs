using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Pool<T> where T : Component, IPoolObject
    {
        private Dictionary<GameObject, Stack<T>> _objects;
        private Dictionary<T, List<GameObject>> _prefabs;

        public Pool(List<PoolInitData> poolDatas)
        {
            foreach (var data in poolDatas)
                for (var i = 0; i < data.Count; i++)
                    Create(data.Prefab);
        }

        private T Create(GameObject prefab)
        {
            var go = Object.Instantiate(prefab); //TODO написать правильное создание префаба
            go.SetActive(false);
            var obj = go.GetComponent<T>();

            if (!_objects.TryGetValue(prefab, out var stack))
            {
                stack = new Stack<T>();
                _objects[prefab] = stack;
            }

            stack.Push(obj);

            _prefabs[obj].Add(prefab);

            return obj;
        }

        public T Get(GameObject prefab)
        {
            if (!_objects.TryGetValue(prefab, out var stack)) return Create(prefab);

            var obj = stack.Pop();
            obj.gameObject.SetActive(true);
            obj.OnAfterFromPool();

            return obj;
        }

        public void Push(T obj)
        {
            if (_prefabs.TryGetValue(obj, out var prefabs))
            {
                var prefab = prefabs.FirstOrDefault(o => obj.gameObject == o);
                obj.OnBeforeToPool();
                prefab?.SetActive(false);
                _objects[prefab].Push(obj);
            }
        }

        public Stack<T> GetAllObjects(GameObject prefab)
        {
            return _objects[prefab];
        }

        public List<GameObject> GetPrefabs()
        {
            return _prefabs.Values.SelectMany(list => list).ToList();
        }
    }
}