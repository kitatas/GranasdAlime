using System;
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

        [HideInInspector] public char wordChar;
        [HideInInspector] public int wordIndex;
        [HideInInspector] public WordStatus wordStatus;

        public void Init(char message, int index, WordStatus status, Func<bool> isInputState,
            Action<(int index, MoveStatus move)> shift, Action execShift)
        {
            var mainCamera = FindObjectOfType<Camera>();
            var startPointX = (int)transform.localPosition.x;
            var updatePointX = startPointX;
            wordChar = message;
            wordIndex = index;
            wordStatus = status;

            word.text = $"{wordChar}";

            this.OnBeginDragAsObservable()
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
                    cursorPoint.z = -5.0f;
                    transform.position = cursorPoint;

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

            this.OnEndDragAsObservable()
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

                    transform.SetLocalPositionX(updatePointX);
                })
                .AddTo(this);
        }
    }
}