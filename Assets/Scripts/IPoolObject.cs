namespace DefaultNamespace
{
    public interface IPoolObject
    {
        void OnAfterFromPool();
        void OnBeforeToPool();
    }
}