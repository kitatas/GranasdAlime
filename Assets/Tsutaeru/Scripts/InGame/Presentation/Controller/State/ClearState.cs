using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Domain.UseCase;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class ClearState : BaseState
    {
        private readonly QuestionUseCase _questionUseCase;

        public ClearState(QuestionUseCase questionUseCase)
        {
            _questionUseCase = questionUseCase;
        }

        public override GameState state => GameState.Clear;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);

            if (_questionUseCase.IsAllClear())
            {
                return GameState.None;
            }
            else
            {
                return GameState.SetUp;
            }
        }
    }
}