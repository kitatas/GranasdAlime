using System;
using System.Text.RegularExpressions;
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
            await RenderAsync(message, animationTime, token);
        }

        public async UniTask RenderAsync(string message, float animationTime, CancellationToken token)
        {
            await hint
                .DOText(message, animationTime)
                .SetLink(gameObject)
                .SetEase(Ease.Linear)
                .WithCancellation(token);
        }

        public async UniTask ResetAsync(float animationTime, CancellationToken token)
        {
            // タグを除外した文字列
            var hintText = Regex.Replace(hint.text, "<.*?>", "");

            var deleteInterval = animationTime / hintText.Length;

            while (hint.text != "")
            {
                var text = hint.text;
                if (text.EndsWith(">"))
                {
                    var index = text.LastIndexOf("<", StringComparison.Ordinal);
                    if (index != -1)
                    {
                        Set(text.Remove(index, text.Length - index));
                        continue;
                    }
                    else
                    {
                        Set("");
                        break;
                    }
                }
                else
                {
                    Set(text.Remove(text.Length - 1));
                }

                await UniTask.Delay(TimeSpan.FromSeconds(deleteInterval), cancellationToken: token);
            }
        }

        private void Set(string message)
        {
            hint.text = message;
        }

        public async UniTask TweenHeightAsync(float y, float animationTime, CancellationToken token)
        {
            await hint.rectTransform
                .DOAnchorPosY(y, animationTime)
                .SetLink(gameObject)
                .SetEase(Ease.Linear)
                .WithCancellation(token);
        }
    }
}