using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.Common.Presentation.View;
using VContainer.Unity;

namespace Tsutaeru.Common.Presentation.Controller
{
    public sealed class ExceptionController : IInitializable, IDisposable
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly RetryView _retryView;
        private readonly CancellationTokenSource _tokenSource;

        public ExceptionController(LoadingUseCase loadingUseCase, RetryView retryView)
        {
            _loadingUseCase = loadingUseCase;
            _retryView = retryView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _retryView.HideAsync(0.0f, _tokenSource.Token).Forget();
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        public async UniTask PopupRetryAsync(string message, CancellationToken token)
        {
            // ロード表示中の場合があるので、非表示にさせる
            _loadingUseCase.Set(false);

            await _retryView.ShowAndHideAsync(message, UiConfig.POPUP_TIME, token);
        }
    }
}