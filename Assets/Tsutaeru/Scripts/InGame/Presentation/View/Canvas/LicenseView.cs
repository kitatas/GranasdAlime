using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class LicenseView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView close = default;

        public Action hideLicense;

        public async UniTaskVoid InitAsync(float animationTime, CancellationToken token)
        {
            close.push
                .Subscribe(_ => hideLicense?.Invoke())
                .AddTo(this);

            hideLicense += () => HideAsync(animationTime, token).Forget();

            HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }
    }
}