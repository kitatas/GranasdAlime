using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Tsutaeru.Common.Presentation.View;
using Tsutaeru.OutGame;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class ReloadButtonView : BaseButtonView
    {
        public override async UniTaskVoid InitAsync(Action<SeType> playSe, CancellationToken token)
        {
            base.InitAsync(playSe, token).Forget();
            await (
                HideAsync(0.0f, token)
            );
        }

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(image
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear))
                .Join(image.GetComponentInChildren<TextMeshProUGUI>()
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject)
                .WithCancellation(token);

            Activate(true);
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            Activate(false);

            await DOTween.Sequence()
                .Append(image
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear))
                .Join(image.GetComponentInChildren<TextMeshProUGUI>()
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject)
                .WithCancellation(token);
        }
    }
}