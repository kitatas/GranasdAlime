using System;
using DG.Tweening;
using TMPro;
using UniEx;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class WordView : UIBehaviour
    {
        [SerializeField] private TextMeshProUGUI word = default;
        [SerializeField] private CanvasGroup canvasGroup = default;

        [HideInInspector] public char wordChar;
        [HideInInspector] public int wordIndex;
        [HideInInspector] public WordStatus wordStatus;

        public void Init(char message, int index, WordStatus status, Func<bool> isInputState,
            Action<(int index, MoveStatus move)> shift, Action execShift)
        {
            var mainCamera = FindObjectOfType<Camera>();
            var startPointX = (int)transform.localPosition.x;
            var updatePointX = startPointX;
            var focusScale = Vector3.one * 1.2f;
            wordChar = message;
            wordIndex = index;
            wordStatus = status;

            word.text = $"{wordChar}";

            DOTween.Sequence()
                .Append(transform
                    .DOScale(Vector3.one, WordConfig.GENERATE_SPEED)
                    .SetEase(Ease.OutBack))
                .Join(canvasGroup
                    .DOFade(1.0f, WordConfig.GENERATE_SPEED)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject);

            this.OnPointerDownAsObservable()
                .Where(_ =>
                {
                    var isInput = isInputState?.Invoke();
                    return isInput.HasValue && isInput.Value;
                })
                .Subscribe(x =>
                {
                    // 移動前の位置
                    startPointX = (int)transform.localPosition.x;
                    updatePointX = startPointX;

                    transform.ToRectTransform()
                        .DOScale(focusScale, WordConfig.FOCUS_SPEED)
                        .SetEase(Ease.OutBack)
                        .SetLink(gameObject);
                })
                .AddTo(this);

            this.OnDragAsObservable()
                .Where(_ =>
                {
                    var isInput = isInputState?.Invoke();
                    return isInput.HasValue && isInput.Value;
                })
                .Subscribe(x =>
                {
                    var cursorPoint = mainCamera.ScreenToWorldPoint(x.position);
                    cursorPoint.y = 0.0f;
                    transform.position = cursorPoint;
                    transform.SetLocalPositionZ(0.0f);

                    var currentX = transform.localPosition.x;
                    if (currentX >= updatePointX + WordConfig.SHIFT_RANGE)
                    {
                        // 末尾の文字である場合は処理しない
                        if (wordStatus == WordStatus.Last) return;

                        updatePointX += WordConfig.INTERVAL;
                        shift?.Invoke((index: wordIndex, move: MoveStatus.Left));
                    }
                    else if (currentX <= updatePointX - WordConfig.SHIFT_RANGE)
                    {
                        // 先頭の文字である場合は処理しない
                        if (wordStatus == WordStatus.First) return;

                        updatePointX -= WordConfig.INTERVAL;
                        shift?.Invoke((index: wordIndex, move: MoveStatus.Right));
                    }
                })
                .AddTo(this);

            this.OnPointerUpAsObservable()
                .Where(_ =>
                {
                    var isInput = isInputState?.Invoke();
                    return isInput.HasValue && isInput.Value;
                })
                .Subscribe(x =>
                {
                    if (startPointX != updatePointX)
                    {
                        execShift?.Invoke();
                    }

                    transform.ToRectTransform()
                        .DOScale(Vector3.one, WordConfig.FOCUS_SPEED)
                        .SetEase(Ease.OutQuart)
                        .SetLink(gameObject);
                    transform.ToRectTransform()
                        .DOLocalMoveX(updatePointX, WordConfig.FOCUS_SPEED)
                        .SetEase(Ease.OutQuart)
                        .SetLink(gameObject);
                })
                .AddTo(this);
        }

        public void TweenShift(float addValue)
        {
            var endValue = transform.localPosition.x + addValue;
            transform.ToRectTransform()
                .DOLocalMoveX(endValue, WordConfig.SHIFT_SPEED)
                .SetEase(Ease.OutQuint)
                .SetLink(gameObject);
        }

        public void Hide()
        {
            DOTween.Sequence()
                .Append(transform
                    .DOScale(Vector3.one, WordConfig.GENERATE_SPEED)
                    .SetEase(Ease.OutQuart))
                .Join(canvasGroup
                    .DOFade(0.0f, WordConfig.GENERATE_SPEED)
                    .SetEase(Ease.OutQuart))
                .SetLink(gameObject);
        }
    }
}