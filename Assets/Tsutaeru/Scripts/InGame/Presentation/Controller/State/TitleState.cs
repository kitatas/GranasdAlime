using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Presentation.View;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseState
    {
        private readonly ProgressView _progressView;
        private readonly StartButtonView _startButtonView;

        public TitleState(ProgressView progressView, StartButtonView startButtonView)
        {
            _progressView = progressView;
            _startButtonView = startButtonView;
        }

        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _progressView.ResetBackground();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _startButtonView.push.ToUniTask(true, token);
            await _startButtonView.HideAsync(UiConfig.ANIMATION_TIME, token);

            await _progressView.ShowBackgroundAsync(UiConfig.ANIMATION_TIME, token);

            return GameState.SetUp;
        }
    }
}