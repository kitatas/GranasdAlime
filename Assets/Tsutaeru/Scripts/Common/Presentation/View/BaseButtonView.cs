using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Tsutaeru.Common.Presentation.View
{
    public abstract class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private Button button = default;

        private readonly float _animationTime = 0.1f;

        public virtual async UniTaskVoid InitAsync(CancellationToken token)
        {
            var rectTransform = button.transform.ToRectTransform();
            var scale = rectTransform.localScale;

            push.Subscribe(_ =>
                {
                    // 押下時のアニメーション
                    DOTween.Sequence()
                        .Append(rectTransform
                            .DOScale(scale * 0.8f, _animationTime))
                        .Append(rectTransform
                            .DOScale(scale, _animationTime))
                        .SetLink(gameObject);
                })
                .AddTo(this);

            await UniTask.Yield(token);
        }

        public IObservable<Unit> push => button.OnClickAsObservable();

        public Image image => button.image;

        public void Activate(bool value)
        {
            button.enabled = value;
        }
    }
}