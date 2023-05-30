using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.Common.Presentation.View;

namespace Tsutaeru.Common.Presentation.Controller
{
    public sealed class ExceptionController
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly RetryView _retryView;

        public ExceptionController(LoadingUseCase loadingUseCase, RetryView retryView)
        {
            _loadingUseCase = loadingUseCase;
            _retryView = retryView;
        }

        public async UniTaskVoid InitAsync(CancellationToken token)
        {
            _retryView.HideAsync(0.0f, token).Forget();
            await UniTask.Yield(token);
        }

        public async UniTask<ExceptionType> ShowExceptionAsync(Exception exception, CancellationToken token)
        {
            // ロード表示中の場合があるので、非表示にさせる
            _loadingUseCase.Set(false);

            switch (exception)
            {
                case OperationCanceledException:
                    return ExceptionType.None;
                case RetryException:
                    await _retryView.ShowAndHideAsync(exception.Message, UiConfig.POPUP_TIME, token);
                    return ExceptionType.Retry;
                default:
                    return ExceptionType.None;
            }
        }
    }
}