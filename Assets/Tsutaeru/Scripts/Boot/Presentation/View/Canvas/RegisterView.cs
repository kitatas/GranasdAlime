using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Tsutaeru.Boot.Presentation.View
{
    public sealed class RegisterView : BaseCanvasGroupView
    {
        [SerializeField] private TMP_InputField inputField = default;
        [SerializeField] private DecisionButtonView decision = default;

        private string inputName => inputField.text;

        public UniTask<string> DecisionAsync(CancellationToken token)
        {
            return decision.push
                .Select(_ => inputName)
                .ToUniTask(true, token);
        }
    }
}