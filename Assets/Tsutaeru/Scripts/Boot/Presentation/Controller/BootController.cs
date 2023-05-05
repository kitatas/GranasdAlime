using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Boot.Domain.UseCase;
using Tsutaeru.Boot.Presentation.View;
using Tsutaeru.InGame;
using Tsutaeru.OutGame;
using Tsutaeru.OutGame.Domain.UseCase;
using VContainer.Unity;

namespace Tsutaeru.Boot.Presentation.Controller
{
    public sealed class BootController : IInitializable, IDisposable
    {
        private readonly AppVersionUseCase _appVersionUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly RegisterView _registerView;
        private readonly CancellationTokenSource _tokenSource;

        public BootController(AppVersionUseCase appVersionUseCase, LoginUseCase loginUseCase, SceneUseCase sceneUseCase,
            RegisterView registerView)
        {
            _appVersionUseCase = appVersionUseCase;
            _loginUseCase = loginUseCase;
            _sceneUseCase = sceneUseCase;
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

                var isUpdate = await _appVersionUseCase.CheckUpdateAsync(token);
                if (isUpdate)
                {
                    // 強制アップデート
                    return;
                }

                _sceneUseCase.Load(SceneName.Main, LoadType.Direct);
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