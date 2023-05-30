using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Boot.Domain.UseCase;
using Tsutaeru.Boot.Presentation.View;
using Tsutaeru.Common;
using Tsutaeru.Common.Domain.UseCase;

namespace Tsutaeru.Boot.Presentation.Controller
{
    public sealed class LoginState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterView _registerView;

        public LoginState(LoadingUseCase loadingUseCase, LoginUseCase loginUseCase, RegisterView registerView)
        {
            _loadingUseCase = loadingUseCase;
            _loginUseCase = loginUseCase;
            _registerView = registerView;
        }

        public override BootState state => BootState.Login;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _registerView.HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            // ロード表示
            _loadingUseCase.Set(true);

            var isLoginSuccess = await _loginUseCase.LoginAsync(token);
            if (isLoginSuccess == false)
            {
                await RegisterAsync(token);
            }

            return BootState.Check;
        }

        private async UniTask RegisterAsync(CancellationToken token)
        {
            while (true)
            {
                // ロード非表示
                _loadingUseCase.Set(false);

                var userName = await _registerView.DecisionNameAsync(UiConfig.POPUP_TIME, token);

                // ロード表示
                _loadingUseCase.Set(true);

                // 名前登録
                var isSuccess = await _loginUseCase.RegisterAsync(userName, token);
                if (isSuccess)
                {
                    break;
                }
            }
        }
    }
}