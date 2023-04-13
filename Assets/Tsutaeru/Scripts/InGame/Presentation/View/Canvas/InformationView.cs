using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class InformationView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView close = default;
        [SerializeField] private CanvasButtonView credit = default;
        [SerializeField] private CanvasButtonView license = default;
        [SerializeField] private CanvasButtonView policy = default;
        [SerializeField] private CanvasButtonView other = default;

        [SerializeField] private WebviewView webviewView = default;

        public Action hideInformation;
        public Action<string> showCredit;
        public Action<string> showLicense;
        public Action<string> showPolicy;

        public async UniTaskVoid InitAsync(float animationTime, CancellationToken token)
        {
            webviewView.InitAsync(animationTime, token).Forget();

            close.push
                .Subscribe(_ => hideInformation?.Invoke())
                .AddTo(this);

            credit.push
                .Subscribe(_ => showCredit?.Invoke(UrlConfig.CREDIT))
                .AddTo(this);

            license.push
                .Subscribe(_ => showLicense?.Invoke(UrlConfig.LICENSE))
                .AddTo(this);

            policy.push
                .Subscribe(_ => showPolicy?.Invoke(UrlConfig.POLICY))
                .AddTo(this);

            other.push
                .Subscribe(_ => Application.OpenURL(UrlConfig.DEVELOPER_APP))
                .AddTo(this);

            hideInformation += () => HideAsync(animationTime, token).Forget();
            showCredit += x => webviewView.ShowAsync(animationTime, x, token).Forget();
            showLicense += x => webviewView.ShowAsync(animationTime, x, token).Forget();
            showPolicy += x => webviewView.ShowAsync(animationTime, x, token).Forget();

            HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }
    }
}