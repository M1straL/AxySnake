using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private int _score;

    public Action<Food,int> DieEvent;

    public void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.root.CompareTag("Player") || other.transform.root.CompareTag("Wasp"))
        {
            //PlayAnimationClip
            Destroy(this);
            DieEvent(this, _score);
        }
    }
}
