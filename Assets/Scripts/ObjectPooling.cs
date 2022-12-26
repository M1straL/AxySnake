using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace
{
    public class ObjectPooling
    {
        private List<PoolObject> _objects;
        private Transform _objectParents;

        private void AddObject(PoolObject poolObject, Transform parent)
        {
            GameObject go = GameObject.Instantiate(poolObject.gameObject);
            go.name = poolObject.name;
            
            go.transform.SetParent(parent);
            _objects.Add(go.GetComponent<PoolObject>());
            
            go.SetActive(false);
        }

        public void Initialize(int count, PoolObject poolObject, Transform parent)
        {
            _objects = new List<PoolObject>();
            for (int i = 0; i < count; i++)
            {
                AddObject(poolObject, parent);
            }
        }

        public PoolObject GetObject()
        {
            foreach (var poolObject in _objects)
            {
                if (poolObject.enabled)
                {
                    return poolObject;
                }
            }
            AddObject(_objects[0], _objectParents );
            return _objects[_objects.Count-1];
        }
    }
}