using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class CautionView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView close = default;
        [SerializeField] private CanvasButtonView decision = default;

        public Action hideCaution;
        public Action delete;

        public async UniTaskVoid InitAsync(float animationTime, CancellationToken token)
        {
            close.push
                .Subscribe(_ => hideCaution?.Invoke())
                .AddTo(this);

            decision.push
                .Subscribe(_ => delete?.Invoke())
                .AddTo(this);

            hideCaution += () => HideAsync(animationTime, token).Forget();
            delete += () =>
            {
                // TODO: delete function
            };

            HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }
    }
}