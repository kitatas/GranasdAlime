using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Base.Presentation.View;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class CautionView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView close = default;

        public Action hideCaution;

        public async UniTaskVoid InitAsync(float animationTime, CancellationToken token)
        {
            close.push
                .Subscribe(_ => hideCaution?.Invoke())
                .AddTo(this);

            hideCaution += () => HideAsync(animationTime, token).Forget();

            HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }
    }
}