using System;
using Tsutaeru.Common.Domain.UseCase;
using UniRx;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class StateUseCase : BaseModelUseCase<GameState>
    {
        public IObservable<GameState> gameState => property.Where(x => x != GameState.None);
    }
}