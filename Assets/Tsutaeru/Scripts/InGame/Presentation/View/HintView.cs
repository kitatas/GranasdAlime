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
            var hintText = Regex.Replace(hint.text, @"[^\p{IsKatakana}\s]", "");

            var deleteInterval = animationTime / hintText.Length;

            while (hint.text != "")
            {
                var text = hint.text;
                var c = text[^1];

                // 末尾の文字を削除
                hint.text = text.Remove(text.Length - 1);

                // タグ以外の文字を削除した場合
                if (char.IsWhiteSpace(c) || (c >= '\u30A1' && c <= '\u30F6'))
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(deleteInterval), cancellationToken: token);
                }
            }
        }

        private void Set(string message)
        {
            hint.text = message;
        }
    }
}