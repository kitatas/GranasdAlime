using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class TitleView : MonoBehaviour
    {
        [SerializeField] private ConfigView configView = default;
        [SerializeField] private LicenseView licenseView = default;
        [SerializeField] private TopView topView = default;

        public async UniTaskVoid InitAsync(float animationTime, CancellationToken token)
        {
            configView.InitAsync(animationTime, token).Forget();
            licenseView.InitAsync(animationTime, token).Forget();
            topView.InitAsync(animationTime, token).Forget();

            configView.hideConfig += () => topView.ShowAsync(animationTime, token).Forget();
            licenseView.hideLicense += () => topView.ShowAsync(animationTime, token).Forget();
            topView.showConfig += () => configView.ShowAsync(animationTime, token).Forget();
            topView.showLicense += () => licenseView.ShowAsync(animationTime, token).Forget();

            await UniTask.Yield(token);
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await topView.HideAsync(animationTime, token);
        }
    }
}