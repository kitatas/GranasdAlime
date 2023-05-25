using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Tsutaeru.Common.Presentation.Presenter
{
    public sealed class LoadingPresenter : IInitializable, IDisposable
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly LoadingView _loadingView;
        private readonly CancellationTokenSource _tokenSource;

        public LoadingPresenter(LoadingUseCase loadingUseCase, LoadingView loadingView)
        {
            _loadingUseCase = loadingUseCase;
            _loadingView = loadingView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _loadingUseCase.property
                .Subscribe(x =>
                {
                    if (x)
                    {
                        _loadingView.ShowAsync(UiConfig.POPUP_TIME, _tokenSource.Token).Forget();
                    }
                    else
                    {
                        _loadingView.HideAsync(UiConfig.POPUP_TIME, _tokenSource.Token).Forget();
                    }
                })
                .AddTo(_loadingView);

            // 非表示で初期化する
            _loadingUseCase.Set(false);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}