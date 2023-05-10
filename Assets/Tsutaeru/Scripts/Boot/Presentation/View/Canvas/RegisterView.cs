using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using Tsutaeru.Base.Presentation.View;
using UniRx;
using UnityEngine;

namespace Tsutaeru.Boot.Presentation.View
{
    public sealed class RegisterView : BaseCanvasGroupView
    {
        [SerializeField] private TMP_InputField inputField = default;
        [SerializeField] private DecisionButtonView decision = default;

        private string inputName => inputField.text;

        public async UniTask<string> DecisionNameAsync(float animationTime, CancellationToken token)
        {
            inputField.text = $"";
            await ShowAsync(animationTime, token);

            // 決定ボタン押下待ち
            var userName = await decision.push.Select(_ => inputName).ToUniTask(true, token);
            await HideAsync(animationTime, token);

            return userName;
        }
    }
}