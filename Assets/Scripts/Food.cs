using DefaultNamespace;
using UniRx;
using UnityEngine;
using IObserver = DefaultNamespace.IObserver<Food>;

public class Food : MonoBehaviour, IPoolObject
{
    [SerializeField] private int _prefabScore;

    private Ctx _ctx;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.root.CompareTag("Untagged") || other.transform.root.CompareTag("Food")) return;


        if (other.transform.root.CompareTag("Player"))
            //TODO Замечание от Сережи var comp = other.transform.GetComponentInParent(Icollacble);
            // comp.Apply()
            _ctx.Score.Execute(_prefabScore);

        _ctx.Died.Execute();
    }


    public void SetCtx(Ctx ctx)
    {
        _ctx = ctx;
    }

    public struct Ctx
    {
        public ReactiveCommand<int> Score;
        public ReactiveCommand Died;
    }


    #region PoolActions

    public void OnAfterFromPool()
    {
        //TODO имплементировать
    }

    public void OnBeforeToPool()
    {
        //TODO имплементировать
    }

    #endregion
}