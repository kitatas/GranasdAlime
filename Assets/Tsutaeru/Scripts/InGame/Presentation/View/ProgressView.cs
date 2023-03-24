using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class ProgressView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI progress = default;
        [SerializeField] private Image background = default;

        public void Render(int value)
        {
            progress.text = $"{value:00} / {GameConfig.MAX_QUESTION}";
        }

        public void ResetBackground()
        {
            DOTween.Sequence()
                .Append(background.rectTransform
                    .DOAnchorPosX(10.0f, 0.0f))
                .Join(background.rectTransform
                    .DOScaleX(0.0f, 0.0f))
                .SetLink(gameObject);
        }

        public async UniTask ShowBackgroundAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(background.rectTransform
                    .DOAnchorPosX(90.0f, animationTime))
                .Join(background.rectTransform
                    .DOScaleX(1.0f, animationTime))
                .SetEase(Ease.OutQuart)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public async UniTask HideBackgroundAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(background.rectTransform
                    .DOAnchorPosX(170.0f, animationTime))
                .Join(background.rectTransform
                    .DOScaleX(0.0f, animationTime))
                .SetEase(Ease.InQuart)
                .SetLink(gameObject)
                .WithCancellation(token);
        }
    }
}