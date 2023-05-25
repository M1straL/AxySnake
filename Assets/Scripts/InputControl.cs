using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputControl : MonoBehaviour
    {
        private Ctx _ctx;

        private void Update()
        {
            _ctx.Vertical.Execute(Input.GetAxis("Vertical"));
            _ctx.Horizontal.Execute(Input.GetAxis("Horizontal"));
        }

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void Init(Ctx ctx)
        {
            ctx.Horizontal = new ReactiveCommand<float>();
            ctx.Vertical = new ReactiveCommand<float>();
        }

        public struct Ctx
        {
            public ReactiveCommand<float> Horizontal;
            public ReactiveCommand<float> Vertical;
        }

        // verticalInput = new ReactiveProperty<float>(vertical); //Input.GetAxis("Horizontal");
        // horizontalInput = new ReactiveProperty<float>(horizontal); //Input.GetAxis("Vertical");
    }
}