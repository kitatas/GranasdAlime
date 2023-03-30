using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tsutaeru.Common.Presentation.View;
using UnityEngine.UI;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class RetryButtonView : BaseButtonView
    {
        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(image
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear))
                .Join(image.GetComponentInChildren<Graphic>()
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
                .Join(image.GetComponentInChildren<Graphic>()
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject)
                .WithCancellation(token);
        }
    }
}