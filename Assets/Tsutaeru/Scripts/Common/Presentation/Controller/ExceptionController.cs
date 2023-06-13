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
        private readonly SceneUseCase _sceneUseCase;
        private readonly CrashView _crashView;
        private readonly RebootView _rebootView;
        private readonly RetryView _retryView;

        public ExceptionController(LoadingUseCase loadingUseCase, SceneUseCase sceneUseCase,
            CrashView crashView, RebootView rebootView, RetryView retryView)
        {
            _loadingUseCase = loadingUseCase;
            _sceneUseCase = sceneUseCase;
            _crashView = crashView;
            _rebootView = rebootView;
            _retryView = retryView;
        }

        public async UniTaskVoid InitAsync(CancellationToken token)
        {
            _crashView.HideAsync(0.0f, token).Forget();
            _rebootView.HideAsync(0.0f, token).Forget();
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
                    // Cancel時は何もしない
                    return ExceptionType.Cancel;
                case RetryException:
                    await _retryView.ShowAndHideAsync(exception.Message, UiConfig.POPUP_TIME, token);
                    return ExceptionType.Retry;
                case RebootException:
                    await _rebootView.ShowAndHideAsync(exception.Message, UiConfig.POPUP_TIME, token);
                    _sceneUseCase.Load(SceneName.Boot, LoadType.Fade);
                    return ExceptionType.Reboot;
                default:
                    // 予期せぬ例外のため、強制終了させる
                    await _crashView.ShowAndPushAsync(exception.Message, UiConfig.POPUP_TIME, token);
#if UNITY_ANDROID
                    UnityEngine.Application.Quit();
#else
                    // android以外の挙動は追記する
#endif
                    return ExceptionType.Crash;
            }
        }
    }
}