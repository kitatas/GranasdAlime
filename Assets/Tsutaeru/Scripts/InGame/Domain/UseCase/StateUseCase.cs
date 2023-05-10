using System;
using Tsutaeru.Base.Domain.UseCase;
using Tsutaeru.InGame.Data.Entity;
using UniRx;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class StateUseCase : BaseModelUseCase<GameState>
    {
        private readonly StateEntity _stateEntity;

        public StateUseCase(StateEntity stateEntity)
        {
            _stateEntity = stateEntity;

            Set(GameConfig.INIT_STATE);
        }

        public IObservable<GameState> gameState => property.Where(x => x != GameState.None);

        public override void Set(GameState state)
        {
            base.Set(state);
            _stateEntity.Set(state);
        }
    }
}