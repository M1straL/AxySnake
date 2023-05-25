using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIManager : MonoBehaviour
    {
        private Ctx _ctx;

        private void Awake()
        {
        }

        private void Update()
        {
        }

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        public struct Ctx
        {
            public ReactiveProperty<int> Lives;
            public ReactiveProperty<int> Score;
        }
    }
}