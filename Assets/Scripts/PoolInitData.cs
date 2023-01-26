using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct PoolInitData
    {
        [SerializeField] public GameObject Prefab;

        [SerializeField] public int Count;

        [SerializeField] public int MaxCountOnLevel;


        public PoolInitData(GameObject prefab, int count, int maxCountOnLevel)
        {
            Prefab = prefab;
            Count = count;
            MaxCountOnLevel = maxCountOnLevel;
        }
    }
}