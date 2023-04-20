using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class ConfigView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView close = default;
        [SerializeField] private CanvasButtonView delete = default;

        [SerializeField] private CautionView cautionView = default;

        public Action hideConfig;
        public Action showCaution;

        public async UniTaskVoid InitAsync(float animationTime, CancellationToken token)
        {
            cautionView.InitAsync(animationTime, token).Forget();

            close.push
                .Subscribe(_ => hideConfig?.Invoke())
                .AddTo(this);

            delete.push
                .Subscribe(_ => showCaution?.Invoke())
                .AddTo(this);

            hideConfig += () => HideAsync(animationTime, token).Forget();
            showCaution += () => cautionView.ShowAsync(animationTime, token).Forget();

            HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }
    }
}