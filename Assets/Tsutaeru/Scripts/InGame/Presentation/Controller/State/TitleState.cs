using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Presentation.View;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseState
    {
        private readonly ConfigView _configView;
        private readonly LicenseView _licenseView;
        private readonly TopView _topView;
        private readonly ProgressView _progressView;
        private readonly StartButtonView _startButtonView;

        public TitleState(ConfigView configView, LicenseView licenseView, TopView topView, ProgressView progressView,
            StartButtonView startButtonView)
        {
            _configView = configView;
            _licenseView = licenseView;
            _topView = topView;
            _progressView = progressView;
            _startButtonView = startButtonView;
        }

        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _configView.InitAsync(UiConfig.POPUP_TIME, token).Forget();
            _licenseView.InitAsync(UiConfig.POPUP_TIME, token).Forget();
            _topView.InitAsync(UiConfig.POPUP_TIME, token).Forget();

            _configView.hideConfig += () => _topView.ShowAsync(UiConfig.POPUP_TIME, token).Forget();
            _licenseView.hideLicense += () => _topView.ShowAsync(UiConfig.POPUP_TIME, token).Forget();
            _topView.showConfig += () => _configView.ShowAsync(UiConfig.POPUP_TIME, token).Forget();
            _topView.showLicense += () => _licenseView.ShowAsync(UiConfig.POPUP_TIME, token).Forget();

            _progressView.ResetBackground();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _startButtonView.push.ToUniTask(true, token);
            await _topView.HideAsync(UiConfig.POPUP_TIME, token);

            await _progressView.ShowBackgroundAsync(UiConfig.ANIMATION_TIME, token);

            return GameState.SetUp;
        }
    }
}