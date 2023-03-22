using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.InGame.Data.Entity;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class TimeUseCase : BaseModelUseCase<float>
    {
        private readonly StateEntity _stateEntity;

        public TimeUseCase(StateEntity stateEntity)
        {
            _stateEntity = stateEntity;
        }

        public void Tick(float deltaTime)
        {
            if (_stateEntity.IsState(GameState.Input))
            {
                Set(value + deltaTime);
            }
        }

        public float value => property.Value;
    }
}