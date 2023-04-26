using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Boot.Domain.UseCase;
using VContainer.Unity;

namespace Tsutaeru.Boot.Presentation.Controller
{
    public sealed class BootController : IInitializable, IDisposable
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly CancellationTokenSource _tokenSource;

        public BootController(LoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;

            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            BootAsync(_tokenSource.Token).Forget();
        }

        private async UniTaskVoid BootAsync(CancellationToken token)
        {
            try
            {
                var isLoginSuccess = await _loginUseCase.LoginAsync(token);
                if (isLoginSuccess == false)
                {
                    UnityEngine.Debug.Log($"register user name");
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"login failed: {e}");
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