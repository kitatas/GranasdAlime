using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;
using UniRx;

namespace Tsutaeru.InGame.Presentation.Controller.TimeAttack
{
    public sealed class ClearState : BaseState
    {
        private readonly ClearUseCase _clearUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly SoundUseCase _soundUseCase;
        private readonly QuestionUseCase _questionUseCase;
        private readonly WordUseCase _wordUseCase;
        private readonly HintView _hintView;
        private readonly ProgressView _progressView;
        private readonly TimeView _timeView;
        private readonly RetryButtonView _retryButtonView;

        public ClearState(ClearUseCase clearUseCase, SceneUseCase sceneUseCase, SoundUseCase soundUseCase,
            QuestionUseCase questionUseCase, WordUseCase wordUseCase, HintView hintView, ProgressView progressView,
            TimeView timeView, RetryButtonView retryButtonView)
        {
            _clearUseCase = clearUseCase;
            _sceneUseCase = sceneUseCase;
            _soundUseCase = soundUseCase;
            _questionUseCase = questionUseCase;
            _wordUseCase = wordUseCase;
            _hintView = hintView;
            _progressView = progressView;
            _timeView = timeView;
            _retryButtonView = retryButtonView;
        }

        public override GameState state => GameState.TaClear;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _progressView.Render(_questionUseCase.progress);
            _timeView.ResetBackground();

            _retryButtonView.ShowAsync(0.0f, token).Forget();
            _retryButtonView.push
                .Subscribe(_ => _sceneUseCase.Reload())
                .AddTo(_retryButtonView);

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _soundUseCase.PlaySe(SeType.Correct);
            _questionUseCase.IncreaseProgress();
            await _wordUseCase.HideAllWordBackgroundAsync(token);

            _soundUseCase.PlaySe(SeType.Slide);
            _soundUseCase.PlaySe(SeType.Hint);
            await (
                _hintView.ResetAsync(UiConfig.ANIMATION_TIME, token),
                _timeView.ShowBackgroundAsync(UiConfig.ANIMATION_TIME, token)
            );

            _soundUseCase.PlaySe(SeType.Hint);
            await _hintView.RenderAsync(_clearUseCase.GetClearMessage(), UiConfig.ANIMATION_TIME, token);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            if (_questionUseCase.IsAllClear())
            {
                _soundUseCase.PlaySe(SeType.Hint);
                await _hintView.ResetAsync(UiConfig.ANIMATION_TIME, token);
                await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

                _soundUseCase.PlaySe(SeType.Slide);
                await (
                    _progressView.HideBackgroundAsync(UiConfig.ANIMATION_TIME, token),
                    _timeView.HideBackgroundAsync(UiConfig.ANIMATION_TIME, token),
                    _retryButtonView.HideAsync(UiConfig.ANIMATION_TIME, token)
                );

                _soundUseCase.PlayBgm(BgmType.Result);

                _soundUseCase.PlaySe(SeType.Hint);
                await _hintView.RenderAsync("Thank you for playing!!", UiConfig.ANIMATION_TIME, token);
                await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

                await _wordUseCase.HideAllWordAsync(token);

                _soundUseCase.PlaySe(SeType.Hint);
                await _hintView.ResetAsync(UiConfig.ANIMATION_TIME, token);

                return GameState.TaFinish;
            }
            else
            {
                _soundUseCase.PlaySe(SeType.Hint);
                await (
                    _wordUseCase.HideAllWordAsync(token),
                    _hintView.ResetAsync(UiConfig.ANIMATION_TIME, token)
                );

                _soundUseCase.PlaySe(SeType.Slide);
                await _timeView.HideBackgroundAsync(UiConfig.ANIMATION_TIME, token);

                _soundUseCase.PlaySe(SeType.ProgressUp);
                _progressView.Render(_questionUseCase.progress);

                return GameState.TaSetUp;
            }
        }
    }
}