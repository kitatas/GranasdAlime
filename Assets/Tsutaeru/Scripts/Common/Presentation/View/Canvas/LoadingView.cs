using DG.Tweening;
using Tsutaeru.Base.Presentation.View;
using UnityEngine;

namespace Tsutaeru.Common.Presentation.View
{
    public sealed class LoadingView : BaseCanvasGroupView
    {
        [SerializeField] private RectTransform loadingIcon = default;

        private void Awake()
        {
            var endValue = new Vector3(0.0f, 0.0f, -360.0f);
            loadingIcon.DORotate(endValue, 1.0f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
                .SetLink(gameObject);
        }
    }
}