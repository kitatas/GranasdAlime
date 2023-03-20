using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class WordView : UIBehaviour
    {
        [SerializeField] private TextMeshProUGUI word = default;

        public void Init(char message)
        {
            word.text = $"{message}";

            var mainCamera = FindObjectOfType<Camera>();
            var startPoint = transform.position;

            this.OnBeginDragAsObservable()
                .Subscribe(x =>
                {
                    // 移動前の位置
                    startPoint = transform.position;
                })
                .AddTo(this);

            this.OnDragAsObservable()
                .Subscribe(x =>
                {
                    var cursorPoint = mainCamera.ScreenToWorldPoint(x.position);
                    cursorPoint.y = 0.0f;
                    cursorPoint.z = -5.0f;
                    transform.position = cursorPoint;
                })
                .AddTo(this);

            this.OnEndDragAsObservable()
                .Subscribe(x =>
                {
                    // TODO: 入れ替え実行
                    transform.position = startPoint;
                })
                .AddTo(this);
        }
    }
}