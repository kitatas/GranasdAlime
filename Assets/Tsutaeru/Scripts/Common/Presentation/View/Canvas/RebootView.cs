using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using Tsutaeru.Base.Presentation.View;
using UnityEngine;

namespace Tsutaeru.Common.Presentation.View
{
    public sealed class RebootView : BaseCanvasGroupView
    {
        [SerializeField] private TextMeshProUGUI messageText = default;
        [SerializeField] private ExceptionButtonView exceptionButton = default;

        public async UniTask ShowAndHideAsync(string message, float animationTime, CancellationToken token)
        {
            messageText.text = $"{message}";

            await ShowAsync(animationTime, token);

            await exceptionButton.push.ToUniTask(true, token);

            await HideAsync(animationTime, token);
        }
    }
}