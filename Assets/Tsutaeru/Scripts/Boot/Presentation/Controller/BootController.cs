using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Boot.Domain.UseCase;
using Tsutaeru.Boot.Presentation.View;
using Tsutaeru.InGame;
using VContainer.Unity;

namespace Tsutaeru.Boot.Presentation.Controller
{
    public sealed class BootController : IInitializable, IDisposable
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterView _registerView;
        private readonly CancellationTokenSource _tokenSource;

        public BootController(LoginUseCase loginUseCase, RegisterView registerView)
        {
            _loginUseCase = loginUseCase;
            _registerView = registerView;

            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _registerView.HideAsync(0.0f, _tokenSource.Token).Forget();

            BootAsync(_tokenSource.Token).Forget();
        }

        private async UniTaskVoid BootAsync(CancellationToken token)
        {
            try
            {
                var isLoginSuccess = await _loginUseCase.LoginAsync(token);
                if (isLoginSuccess == false)
                {
                    await RegisterAsync(_tokenSource.Token);
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"login failed: {e}");
                throw;
            }
        }

        private async UniTask RegisterAsync(CancellationToken token)
        {
            while (true)
            {
                await _registerView.ShowAsync(UiConfig.POPUP_TIME, token);

                // 決定ボタン押下待ち
                var userName = await _registerView.DecisionAsync(token);
                await _registerView.HideAsync(UiConfig.POPUP_TIME, token);

                // 名前登録
                var isSuccess = await _loginUseCase.RegisterAsync(userName, token);
                if (isSuccess)
                {
                    break;
                }
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}