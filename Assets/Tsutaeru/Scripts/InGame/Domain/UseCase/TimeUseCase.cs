using Tsutaeru.Base.Domain.UseCase;
using Tsutaeru.InGame.Data.Entity;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class TimeUseCase : BaseModelUseCase<float>
    {
        private readonly StateEntity _stateEntity;
        private readonly TimeEntity _timeEntity;

        public TimeUseCase(StateEntity stateEntity, TimeEntity timeEntity)
        {
            _stateEntity = stateEntity;
            _timeEntity = timeEntity;
        }

        public void Tick(float deltaTime)
        {
            if (_stateEntity.IsState(GameState.Input))
            {
                _timeEntity.Add(deltaTime);
                Set(_timeEntity.value);
            }
        }
    }
}