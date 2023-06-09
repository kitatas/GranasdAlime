using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;

namespace Tsutaeru.InGame.Presentation.Controller.TimeAttack
{
    public sealed class SetUpState : BaseState
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly QuestionUseCase _questionUseCase;
        private readonly WordUseCase _wordUseCase;
        private readonly HintView _hintView;

        public SetUpState(SoundUseCase soundUseCase, QuestionUseCase questionUseCase, WordUseCase wordUseCase,
            HintView hintView)
        {
            _soundUseCase = soundUseCase;
            _questionUseCase = questionUseCase;
            _wordUseCase = wordUseCase;
            _hintView = hintView;
        }

        public override GameState state => GameState.TaSetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _hintView.Init();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            var data = _questionUseCase.Lot();

            _soundUseCase.PlaySe(SeType.Hint);
            await _hintView.RenderAsync(data, UiConfig.ANIMATION_TIME, token);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            await _wordUseCase.BuildAsync(data, _soundUseCase.PlaySe, token);

            await UniTask.Yield(token);

            return GameState.TaInput;
        }
    }
}