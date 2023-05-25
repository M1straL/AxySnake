using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public class Root
    {
        private GameManager _gameManager;
        private InputControl _inputControl;
        private LevelManager _levelManager;
        private UIManager _uiManager;

        public void Init()
        {
            var gameManager = new GameManager(new GameManager.Ctx
            {
                IsLevelFailed = _isLevelFailed,
                _lives = _lives,
                _score = _score
            });

            var levelManager = new LevelManager(new LevelManager.Ctx
            {
                HorizontalInput = _horizontalInput,
                VerticalInput = _verticalInput,
                IsLevelFailed = _isLevelFailed
            });

            var uiGO = new GameObject("UiManager");
            var uiManager = uiGO.AddComponent<UIManager>();
            uiManager.SetCtx(new UIManager.Ctx
            {
                Lives = _lives,
                Score = _score
            });

            var inputControl = new GameObject("InputControl");
            var input = inputControl.AddComponent<InputControl>();

            input.SetCtx(new InputControl.Ctx
            {
                Horizontal = _horizontalInput,
                Vertical = _verticalInput
            });
        }

        #region DataChannels

        private ReactiveCommand<float> _verticalInput;
        private ReactiveCommand<float> _horizontalInput;
        private ReactiveCommand _isLevelFailed;

        private ReactiveProperty<int> _lives;
        private ReactiveProperty<int> _score;

        #endregion
    }
}