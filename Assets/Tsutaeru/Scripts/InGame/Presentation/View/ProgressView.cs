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