using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class Pool<T> where T : Component, IPoolObject
    {
        private readonly Dictionary<GameObject, Stack<T>> _objects;
        private readonly Dictionary<Type, List<GameObject>> _prefabs;

        public Pool(List<PoolInitData> poolDatas)
        {
            _objects = new Dictionary<GameObject, Stack<T>>();
            _prefabs = new Dictionary<Type, List<GameObject>>();

            foreach (var data in poolDatas)
                for (var i = 0; i < data.Count; i++)
                    Create(data.Prefab);
        }

        private T Create(GameObject prefab)
        {
            var go = Object.Instantiate(prefab);
            go.SetActive(false);
            var obj = go.GetComponent<T>();

            if (!_objects.TryGetValue(prefab, out var stack))
            {
                stack = new Stack<T>();
                _objects[prefab] = stack;
            }

            stack.Push(obj);

            var objectType = obj.GetType();
            var hashCode = obj.GetHashCode();
            if (!_prefabs.TryGetValue(objectType, out var gameObjects))
            {
                gameObjects = new List<GameObject>();
                _prefabs[objectType] = gameObjects;
            }

            gameObjects.Add(prefab);

            return obj;
        }

        public T Get(GameObject prefab)
        {
            if (!_objects.TryGetValue(prefab, out var stack)) return Create(prefab);

            if (!stack.TryPop(out var obj)) return Create(prefab);

            obj.gameObject.SetActive(true);
            obj.OnAfterFromPool();

            return obj;
        }

        public void Push(T obj)
        {
            if (_prefabs.TryGetValue(obj.GetType(), out var prefabs))
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