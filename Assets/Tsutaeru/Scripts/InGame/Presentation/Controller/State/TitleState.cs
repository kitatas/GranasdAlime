using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseState
    {
        private readonly GameModeUseCase _gameModeUseCase;
        private readonly SoundUseCase _soundUseCase;
        private readonly ProgressView _progressView;
        private readonly TitleView _titleView;

        public TitleState(GameModeUseCase gameModeUseCase, SoundUseCase soundUseCase, ProgressView progressView,
            TitleView titleView)
        {
            _gameModeUseCase = gameModeUseCase;
            _soundUseCase = soundUseCase;
            _progressView = progressView;
            _titleView = titleView;
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
            var mode = await _titleView.SelectGameMode().ToUniTask(true, token);
            _gameModeUseCase.SetUp(mode);

            await _titleView.HideAsync(UiConfig.POPUP_TIME, token);

            _soundUseCase.PlayBgm(BgmType.Main);

            _soundUseCase.PlaySe(SeType.Slide);
            await _progressView.ShowBackgroundAsync(UiConfig.ANIMATION_TIME, token);

            return GameState.SetUp;
        }
    }
}