using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;
using Tsutaeru.OutGame;
using Tsutaeru.OutGame.Domain.UseCase;
using UniRx;
using VContainer.Unity;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class UserDataController : IInitializable, IDisposable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly UserDataUseCase _userDataUseCase;
        private readonly AccountDeleteView _accountDeleteView;
        private readonly NameInputView _nameInputView;

        private readonly CancellationTokenSource _tokenSource;

        public UserDataController(SceneUseCase sceneUseCase, UserDataUseCase userDataUseCase,
            AccountDeleteView accountDeleteView, NameInputView nameInputView)
        {
            _sceneUseCase = sceneUseCase;
            _userDataUseCase = userDataUseCase;
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
                    try
                    {
                        UpdateAsync(x, _tokenSource.Token).Forget();
                    }
                    catch (Exception e)
                    {
                        // TODO: リトライ
                        UnityEngine.Debug.LogError($"update user name: {e}");
                        throw;
                    }
                })
                .AddTo(_tokenSource.Token);
        }

        private async UniTaskVoid UpdateAsync(string name, CancellationToken token)
        {
            await _userDataUseCase.UpdateUserNameAsync(name, token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}