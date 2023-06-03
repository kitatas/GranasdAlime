using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Tsutaeru.Common.Presentation.Controller
{
    public sealed class CanvasController : MonoBehaviour
    {
        [SerializeField] private Canvas canvas = default;

        private void Start()
        {
            SetRenderCamera();
        }

        private void SetRenderCamera()
        {
            this.UpdateAsObservable()
                .Where(_ => canvas.worldCamera == null)
                .Subscribe(_ =>
                {
                    canvas.worldCamera = FindObjectOfType<Camera>();
                    canvas.sortingLayerName = "UI";
                    canvas.sortingOrder = 999;
                })
                .AddTo(this);
        }
    }
}