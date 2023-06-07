using System;
using Tsutaeru.Common;
using Tsutaeru.InGame.Data.Entity;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class GameModeUseCase
    {
        private readonly GameModeEntity _gameModeEntity;
        private readonly TimeEntity _timeEntity;

        public GameModeUseCase(GameModeEntity gameModeEntity, TimeEntity timeEntity)
        {
            _gameModeEntity = gameModeEntity;
            _timeEntity = timeEntity;
        }

        public void SetUp(GameMode mode)
        {
            var initTime = mode switch
            {
                GameMode.TimeAttack  => 0.0f,
                GameMode.ScoreAttack => GameConfig.MAX_TIME,
                _ => throw new Exception(ExceptionConfig.UNMATCHED_GAME_MODE),
            };
            _timeEntity.Set(initTime);

            _gameModeEntity.Set(mode);
        }
    }
}