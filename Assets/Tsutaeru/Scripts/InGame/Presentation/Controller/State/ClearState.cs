using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class ClearState : BaseState
    {
        private readonly QuestionUseCase _questionUseCase;
        private readonly WordUseCase _wordUseCase;
        private readonly HintView _hintView;
        private readonly ProgressView _progressView;
        private readonly TimeView _timeView;

        public ClearState(QuestionUseCase questionUseCase, WordUseCase wordUseCase, HintView hintView,
            ProgressView progressView, TimeView timeView)
        {
            _questionUseCase = questionUseCase;
            _wordUseCase = wordUseCase;
            _hintView = hintView;
            _progressView = progressView;
            _timeView = timeView;
        }

        public override GameState state => GameState.Clear;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _progressView.Render(_questionUseCase.progress);
            _timeView.ResetBackground();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _wordUseCase.HideAllWordBackgroundAsync(token);

            await _timeView.ShowBackgroundAsync(UiConfig.ANIMATION_TIME, token);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            if (_questionUseCase.IsAllClear())
            {
                await _hintView.ResetAsync(UiConfig.ANIMATION_TIME, token);
                await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

                await (
                    _progressView.HideBackgroundAsync(UiConfig.ANIMATION_TIME, token),
                    _timeView.HideBackgroundAsync(UiConfig.ANIMATION_TIME, token)
                );

                await _hintView.RenderAsync("Thank you for playing!!", UiConfig.ANIMATION_TIME, token);
                await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

                await _wordUseCase.HideAllWordAsync(token);

                return GameState.Result;
            }
            else
            {
                await (
                    _wordUseCase.HideAllWordAsync(token),
                    _hintView.ResetAsync(UiConfig.ANIMATION_TIME, token)
                );

                await _timeView.HideBackgroundAsync(UiConfig.ANIMATION_TIME, token);

                _progressView.Render(_questionUseCase.progress);

                return GameState.SetUp;
            }
        }
    }
}