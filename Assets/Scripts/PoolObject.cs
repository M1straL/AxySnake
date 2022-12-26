using UnityEngine;

namespace DefaultNamespace
{
    [AddComponentMenu("Pool/PoolObject")]
    public class PoolObject : MonoBehaviour
    {
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}