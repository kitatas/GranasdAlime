using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Domain.UseCase;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class JudgeState : BaseState
    {
        private readonly WordUseCase _wordUseCase;

        public JudgeState(WordUseCase wordUseCase)
        {
            _wordUseCase = wordUseCase;
        }

        public override GameState state => GameState.Judge;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);

            if (_wordUseCase.IsCorrect())
            {
                return GameState.None;
            }
            else
            {
                return GameState.Input;
            }
        }
    }
}