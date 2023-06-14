using Tsutaeru.Base.Domain.UseCase;
using Tsutaeru.Common;
using Tsutaeru.InGame.Data.Entity;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class TimeUseCase : BaseModelUseCase<float>
    {
        private readonly GameModeEntity _gameModeEntity;
        private readonly StateEntity _stateEntity;
        private readonly TimeEntity _timeEntity;

        public TimeUseCase(GameModeEntity gameModeEntity, StateEntity stateEntity, TimeEntity timeEntity)
        {
            _gameModeEntity = gameModeEntity;
            _stateEntity = stateEntity;
            _timeEntity = timeEntity;
        }

        public void Tick(float deltaTime)
        {
            if (_stateEntity.IsState(GameState.TaInput))
            {
                switch (_gameModeEntity.value)
                {
                    case GameMode.TimeAttack:
                        _timeEntity.Add(deltaTime);
                        break;
                    case GameMode.ScoreAttack:
                        _timeEntity.Subtract(deltaTime);
                        break;
                    default:
                        throw new CrashException(ExceptionConfig.UNMATCHED_GAME_MODE);
                }

                Set(_timeEntity.value);
            }
        }
    }
}