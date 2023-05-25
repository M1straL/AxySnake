using System.Linq;
using DefaultNamespace;
using UniRx;
using UnityEngine;

public class LevelManager
{
    private const string _path = "Assets/Pools/"; // указать путь.

    private readonly LevelsConfig _levelsConfig;
    private FoodManager _foodManager;
    private int _level;
    private PlayerEntity _player;


    public LevelManager(Ctx ctx)
    {
        _levelsConfig = Resources.Load<LevelsConfig>(_path);
        if (_levelsConfig == null) Debug.LogError($"LevelsConfig not found in {_path}");

        var levelData = GetLevelData(_level);
        var foodManager = _foodManager;
        _foodManager.Init(levelData.FoodDatas);

        _player = new PlayerEntity(new PlayerEntity.Ctx
        {
            HorizontalInput = ctx.HorizontalInput,
            VerticalInput = ctx.VerticalInput,
            IsLevelFailed = ctx.IsLevelFailed
        }); // Передать туда рычаги управления, которые View отдает вовне.


        //Создаем скриптабл оьжект и оттуда дергаем префаб игрока уже со
    }

    private LevelsConfig.LevelData GetLevelData(int level)
    {
        return _levelsConfig._levelDatas.FirstOrDefault(data => data._level == level);
    }

    public struct Ctx
    {
        public ReactiveCommand<float> VerticalInput;
        public ReactiveCommand<float> HorizontalInput;
        public ReactiveCommand IsLevelFailed;
    }
}