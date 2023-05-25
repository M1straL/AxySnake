using System;

namespace DefaultNamespace
{
    public interface IObserver<T>
    {
        void HandleEvent(T sender, EventArgs args);
    }
}