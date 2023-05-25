using DefaultNamespace;
using UniRx;
using UnityEngine;

public class PlayerEntity
{
    private readonly float _playerSpeed = 8.0f;
    private readonly float _rotationSpeed = 100.0f;

    private PlayerView _playerView;


    public PlayerEntity(Ctx ctx)
    {
        SubscribeToReactive(ctx);
    }

    private void SubscribeToReactive(Ctx ctx)
    {
        ctx.VerticalInput.Subscribe(f => _playerView.Move(GetMovementDirection(f))).AddTo(this);
        ctx.HorizontalInput.Subscribe(f => _playerView.Rotate(GetRotationVector(f))).AddTo(this);

        var playerRoot = new GameObject("Player");
        playerRoot.transform.position = new Vector3(50F, 0.5F, 50F);
        _playerView = playerRoot.AddComponent<PlayerView>();

        _playerView.SetCtx(new PlayerView.Ctx
        {
            IsLevelFailed = ctx.IsLevelFailed
        });
    }

    private Vector3 GetMovementDirection(float vertical)
    {
        var movementDirection = new Vector3(0, 0, vertical);
        movementDirection.Normalize();
        return movementDirection * (_playerSpeed * Time.deltaTime);
    }

    private Vector3 GetRotationVector(float horizontal)
    {
        return new Vector3(0, horizontal, 0) * (_rotationSpeed * Time.deltaTime);
    }

    public struct Ctx
    {
        public ReactiveCommand<float> VerticalInput;
        public ReactiveCommand<float> HorizontalInput;
        public ReactiveCommand IsLevelFailed;
    }
}