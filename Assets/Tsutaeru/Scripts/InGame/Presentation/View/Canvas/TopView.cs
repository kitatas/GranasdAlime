using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class TopView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView config = default;
        [SerializeField] private CanvasButtonView license = default;

        public Action showConfig;
        public Action showLicense;

        public async UniTaskVoid InitAsync(float animationTime, CancellationToken token)
        {
            config.push
                .Subscribe(_ => showConfig?.Invoke())
                .AddTo(this);

            license.push
                .Subscribe(_ => showLicense?.Invoke())
                .AddTo(this);

            showConfig += () => HideAsync(animationTime, token).Forget();
            showLicense += () => HideAsync(animationTime, token).Forget();

            ShowAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }
    }
}