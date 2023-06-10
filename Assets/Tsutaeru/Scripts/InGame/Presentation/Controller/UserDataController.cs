using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.Common.Presentation.Controller;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class UserDataController : IInitializable, IDisposable
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly UserDataUseCase _userDataUseCase;
        private readonly ExceptionController _exceptionController;
        private readonly AccountDeleteView _accountDeleteView;
        private readonly NameInputView _nameInputView;

        private readonly CancellationTokenSource _tokenSource;

        public UserDataController(LoadingUseCase loadingUseCase, SceneUseCase sceneUseCase,
            UserDataUseCase userDataUseCase, ExceptionController exceptionController,
            AccountDeleteView accountDeleteView, NameInputView nameInputView)
        {
            _loadingUseCase = loadingUseCase;
            _sceneUseCase = sceneUseCase;
            _userDataUseCase = userDataUseCase;
            _exceptionController = exceptionController;
            _accountDeleteView = accountDeleteView;
            _nameInputView = nameInputView;

            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _accountDeleteView.DeleteDecision()
                .Subscribe(_ =>
                {
                    _sceneUseCase.Load(SceneName.Boot, LoadType.Fade);
                    _userDataUseCase.Delete();
                })
                .AddTo(_tokenSource.Token);

            _nameInputView.Init(_userDataUseCase.GetUserName());
            _nameInputView.UpdateName()
                .Subscribe(x =>
                {
                    UpdateAsync(x, _tokenSource.Token).Forget();
                })
                .AddTo(_tokenSource.Token);
        }

        private async UniTaskVoid UpdateAsync(string name, CancellationToken token)
        {
            try
            {
                // ロード表示
                _loadingUseCase.Set(true);

                await _userDataUseCase.UpdateUserNameAsync(name, token);

                // ロード非表示
                _loadingUseCase.Set(false);
            }
            catch (Exception e)
            {
                // 更新失敗だけなのでリトライは考慮しない
                UnityEngine.Debug.LogError($"update user name: {e}");
                await _exceptionController.ShowExceptionAsync(e, _tokenSource.Token);

                // 変更前の名前に戻す 
                _nameInputView.Init(_userDataUseCase.GetUserName());
                throw;
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}