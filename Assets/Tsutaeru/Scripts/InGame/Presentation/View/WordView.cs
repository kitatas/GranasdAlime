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

        [HideInInspector] public int wordIndex;
        [HideInInspector] public WordStatus wordStatus;

        public void Init(char message, int index, WordStatus status, Action<(int index, MoveStatus move)> shift)
        {
            word.text = $"{message}";

            var mainCamera = FindObjectOfType<Camera>();
            var startPointX = (int)transform.localPosition.x;
            wordIndex = index;
            wordStatus = status;

            this.OnBeginDragAsObservable()
                .Subscribe(x =>
                {
                    // 移動前の位置
                    startPointX = (int)transform.localPosition.x;
                })
                .AddTo(this);

            this.OnDragAsObservable()
                .Subscribe(x =>
                {
                    var cursorPoint = mainCamera.ScreenToWorldPoint(x.position);
                    cursorPoint.y = 0.0f;
                    cursorPoint.z = -5.0f;
                    transform.position = cursorPoint;

                    var currentX = transform.localPosition.x;
                    if (currentX >= startPointX + WordConfig.SHIFT_RANGE)
                    {
                        // 先頭の文字である場合は処理しない
                        if (wordStatus == WordStatus.Last) return;

                        startPointX += WordConfig.INTERVAL;
                        shift?.Invoke((index: wordIndex, move: MoveStatus.Left));
                    }
                    else if (currentX <= startPointX - WordConfig.SHIFT_RANGE)
                    {
                        // 末尾の文字である場合は処理しない
                        if (wordStatus == WordStatus.First) return;

                        startPointX -= WordConfig.INTERVAL;
                        shift?.Invoke((index: wordIndex, move: MoveStatus.Right));
                    }
                })
                .AddTo(this);

            this.OnEndDragAsObservable()
                .Subscribe(x =>
                {
                    // TODO: 操作完了通知
                    transform.SetLocalPositionX(startPointX);
                })
                .AddTo(this);
        }
    }
}