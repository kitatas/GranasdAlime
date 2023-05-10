using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tsutaeru.Common;
using UniEx;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Tsutaeru.Base.Presentation.View
{
    public abstract class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private Button button = default;
        [SerializeField] private bool isPlaySe = true;

        private readonly float _animationTime = 0.1f;

        public virtual async UniTaskVoid InitAsync(Action<SeType> playSe, CancellationToken token)
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

            push.Where(_ => isPlaySe)
                .Subscribe(_ => playSe?.Invoke(SeType.Decision))
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