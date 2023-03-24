using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UnityEngine;

namespace Tsutaeru.Common.Presentation.View
{
    public abstract class BaseCanvasGroupView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;

        public virtual async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            canvasGroup.blocksRaycasts = true;

            await DOTween.Sequence()
                .Append(canvasGroup
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.OutBack))
                .Join(canvasGroup.transform.ToRectTransform()
                    .DOScale(Vector3.one, animationTime)
                    .SetEase(Ease.OutBack))
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public virtual async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(canvasGroup
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.OutQuart))
                .Join(canvasGroup.transform.ToRectTransform()
                    .DOScale(Vector3.one * 0.8f, animationTime)
                    .SetEase(Ease.OutQuart))
                .SetLink(gameObject)
                .WithCancellation(token);

            canvasGroup.blocksRaycasts = false;
        }
    }
}