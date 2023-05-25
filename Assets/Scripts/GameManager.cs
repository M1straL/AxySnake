using System;
using DefaultNamespace;
using UniRx;

public class GameManager : IDisposable
{
    private readonly ReactiveCommand<int> _addScore;

    private readonly CompositeDisposable _disposable;

    private PlayerEntity _player;

    private PlayerView _playerView;

    private Root _root;

    public GameManager(Ctx ctx)
    {
        _disposable = new CompositeDisposable(); //CTRL + SPACe code gen
        _addScore = new ReactiveCommand<int>();
        ctx._score = new ReactiveProperty<int>(0);

        _addScore.Subscribe(scoreToAdd => ctx._score.Value += scoreToAdd)
            .AddTo(_disposable); //отписка через _dispoable для не Mobnobehaviour
        ctx._lives = new ReactiveProperty<int>(5);

        ctx.IsLevelFailed.Subscribe(unit => { LevelFailed(ctx._lives); }).AddTo(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }

    public PlayerEntity GetPlayer()
    {
        return _player;
    }

    private void LevelFailed(ReactiveProperty<int> lives)
    {
        lives.Value -= 1;
    }

    public struct Ctx
    {
        public ReactiveCommand IsLevelFailed;
        public ReactiveProperty<int> _lives;
        public ReactiveProperty<int> _score;
    }

    #region Channels

    #endregion
}