using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class UserDataController : IInitializable, IDisposable
    {
        private readonly UserDataUseCase _userDataUseCase;
        private readonly NameInputView _nameInputView;

        private readonly CancellationTokenSource _tokenSource;

        public UserDataController(UserDataUseCase userDataUseCase, NameInputView nameInputView)
        {
            _userDataUseCase = userDataUseCase;
            _nameInputView = nameInputView;

            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
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