using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class HintView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hint = default;

        public void Init()
        {
            Set("");
        }

        public async UniTask RenderAsync(Data.DataStore.QuestionData data, float animationTime, CancellationToken token)
        {
            var message = $"{data.originHint.ToWord()} <size=-10>カラ<size=+0> {data.answerHint.ToWord()} <size=-10>ヘ";

            await hint
                .DOText(message, animationTime)
                .SetLink(gameObject)
                .SetEase(Ease.Linear)
                .WithCancellation(token);
        }

        private void Set(string message)
        {
            hint.text = message;
        }
    }
}