using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Tsutaeru.Boot.Presentation.View
{
    public sealed class UpdateView : BaseCanvasGroupView
    {
        [SerializeField] private DecisionButtonView decision = default;

        public async UniTaskVoid InitAsync(CancellationToken token)
        {
            decision.push
                .Subscribe(_ => Application.OpenURL(InGame.UrlConfig.APP))
                .AddTo(this);

            HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }
    }
}