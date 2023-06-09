using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Domain.UseCase;

namespace Tsutaeru.InGame.Presentation.Controller.TimeAttack
{
    public sealed class JudgeState : BaseState
    {
        private readonly WordUseCase _wordUseCase;

        public JudgeState(WordUseCase wordUseCase)
        {
            _wordUseCase = wordUseCase;
        }

        public override GameState state => GameState.TaJudge;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);

            if (_wordUseCase.IsCorrect())
            {
                return GameState.TaClear;
            }
            else
            {
                return GameState.TaInput;
            }
        }
    }
}