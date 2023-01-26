using System;
using DefaultNamespace;
using UnityEngine;

public class Food : MonoBehaviour, IPoolObject
{
    [SerializeField] private Collider _collider;

    [SerializeField] private int _score;

    public Action<Food, int> DieEvent;

    public void Start()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.root.CompareTag("Player") || other.transform.root.CompareTag("Wasp"))
            //PlayAnimationClip
            DieEvent(this, _score);
    }

    public void OnAfterFromPool()
    {
        throw new NotImplementedException();
    }

    public void OnBeforeToPool()
    {
        throw new NotImplementedException();
    }
}