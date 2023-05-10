using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseState
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly ProgressView _progressView;
        private readonly TitleView _titleView;
        private readonly StartButtonView _startButtonView;

        public TitleState(SoundUseCase soundUseCase, TitleView titleView, ProgressView progressView,
            StartButtonView startButtonView)
        {
            _soundUseCase = soundUseCase;
            _progressView = progressView;
            _titleView = titleView;
            _startButtonView = startButtonView;
        }

        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _titleView.InitAsync(UiConfig.POPUP_TIME, token).Forget();

            _progressView.ResetBackground();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _startButtonView.push.ToUniTask(true, token);
            await _titleView.HideAsync(UiConfig.POPUP_TIME, token);

            _soundUseCase.PlayBgm(BgmType.Main);

            _soundUseCase.PlaySe(SeType.Slide);
            await _progressView.ShowBackgroundAsync(UiConfig.ANIMATION_TIME, token);

            return GameState.SetUp;
        }
    }
}