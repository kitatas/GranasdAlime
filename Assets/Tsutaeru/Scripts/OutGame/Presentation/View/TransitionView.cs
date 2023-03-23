using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tsutaeru.OutGame.Presentation.View
{
    public sealed class TransitionView : MonoBehaviour
    {
        [SerializeField] private Image maskUp = default;
        [SerializeField] private Image maskDown = default;
        [SerializeField] private Image raycastBlocker = default;

        public async UniTask FadeInAsync(float animationTime, CancellationToken token)
        {
            raycastBlocker.raycastTarget = true;

            await DOTween.Sequence()
                .Append(maskUp.rectTransform
                    .DOAnchorPosY(0.0f, animationTime))
                .Join(maskDown.rectTransform
                    .DOAnchorPosY(0.0f, animationTime))
                .SetEase(Ease.OutQuart)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public async UniTask FadeOutAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(maskUp.rectTransform
                    .DOAnchorPosY(270.0f, animationTime))
                .Join(maskDown.rectTransform
                    .DOAnchorPosY(-270.0f, animationTime))
                .SetEase(Ease.OutQuart)
                .SetLink(gameObject)
                .WithCancellation(token);

            raycastBlocker.raycastTarget = false;
        }
    }
}