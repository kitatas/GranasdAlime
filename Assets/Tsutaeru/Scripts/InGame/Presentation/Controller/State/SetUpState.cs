using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class SetUpState : BaseState
    {
        private readonly QuestionUseCase _questionUseCase;
        private readonly WordUseCase _wordUseCase;
        private readonly HintView _hintView;

        public SetUpState(QuestionUseCase questionUseCase, WordUseCase wordUseCase,
            HintView hintView)
        {
            _questionUseCase = questionUseCase;
            _wordUseCase = wordUseCase;
            _hintView = hintView;
        }

        public override GameState state => GameState.SetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _hintView.Init();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            var data = _questionUseCase.Lot();
            _hintView.Render(data);
            _wordUseCase.Build(data);

            await UniTask.Yield(token);

            return GameState.Input;
        }
    }
}