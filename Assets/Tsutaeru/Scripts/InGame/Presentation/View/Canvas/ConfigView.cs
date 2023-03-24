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

        public Action hideConfig;

        public async UniTaskVoid InitAsync(float animationTime, CancellationToken token)
        {
            close.push
                .Subscribe(_ => hideConfig?.Invoke())
                .AddTo(this);

            hideConfig += () => HideAsync(animationTime, token).Forget();

            HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }
    }
}