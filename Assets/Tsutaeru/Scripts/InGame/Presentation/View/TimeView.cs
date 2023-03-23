using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Tsutaeru.Common.Presentation.View;
using UnityEngine;
using UnityEngine.UI;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class TimeView : BaseView<float>
    {
        [SerializeField] private TextMeshProUGUI time = default;
        [SerializeField] private Image background = default;

        public override void Render(float value)
        {
            time.text = $"{value:0.00}";
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

            ResetBackground();
        }
    }
}