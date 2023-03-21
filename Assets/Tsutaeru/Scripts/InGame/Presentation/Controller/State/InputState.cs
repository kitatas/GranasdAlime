using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Domain.UseCase;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class InputState : BaseState
    {
        private readonly WordUseCase _wordUseCase;

        public InputState(WordUseCase wordUseCase)
        {
            _wordUseCase = wordUseCase;
        }

        public override GameState state => GameState.Input;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _wordUseCase.ExecShift().ToUniTask(true, token);

            return GameState.None;
        }
    }
}