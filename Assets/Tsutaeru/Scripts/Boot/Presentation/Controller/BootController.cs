using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Boot.Domain.UseCase;
using Tsutaeru.Boot.Presentation.View;
using Tsutaeru.Common;
using Tsutaeru.Common.Domain.UseCase;
using VContainer.Unity;

namespace Tsutaeru.Boot.Presentation.Controller
{
    public sealed class BootController : IInitializable, IDisposable
    {
        private readonly AppVersionUseCase _appVersionUseCase;
        private readonly LoadingUseCase _loadingUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly RegisterView _registerView;
        private readonly UpdateView _updateView;
        private readonly CancellationTokenSource _tokenSource;

        public BootController(AppVersionUseCase appVersionUseCase, LoadingUseCase loadingUseCase,
            LoginUseCase loginUseCase, SceneUseCase sceneUseCase, RegisterView registerView, UpdateView updateView)
        {
            _appVersionUseCase = appVersionUseCase;
            _loadingUseCase = loadingUseCase;
            _loginUseCase = loginUseCase;
            _sceneUseCase = sceneUseCase;
            _registerView = registerView;
            _updateView = updateView;

            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _registerView.HideAsync(0.0f, _tokenSource.Token).Forget();
            _updateView.InitAsync(_tokenSource.Token).Forget();

            BootAsync(_tokenSource.Token).Forget();
        }

        private async UniTaskVoid BootAsync(CancellationToken token)
        {
            try
            {
                // ロード表示
                _loadingUseCase.Set(true);

                var isLoginSuccess = await _loginUseCase.LoginAsync(token);
                if (isLoginSuccess == false)
                {
                    await RegisterAsync(_tokenSource.Token);
                }

                // マスタからバージョンチェック
                var isUpdate = await _appVersionUseCase.CheckUpdateAsync(token);

                // ロード非表示
                _loadingUseCase.Set(false);

                if (isUpdate)
                {
                    // 強制アップデート
                    _updateView.ShowAsync(InGame.UiConfig.POPUP_TIME, token).Forget();
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
                // ロード非表示
                _loadingUseCase.Set(false);

                var userName = await _registerView.DecisionNameAsync(InGame.UiConfig.POPUP_TIME, token);

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

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}