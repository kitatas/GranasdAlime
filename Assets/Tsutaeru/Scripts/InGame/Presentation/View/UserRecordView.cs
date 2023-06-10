using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class UserRecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentScore = default;
        [SerializeField] private TextMeshProUGUI highScore = default;
        [SerializeField] private DecisionButtonView tweetButton = default;

        public void SetScore(float current, float high)
        {
            SetCurrentScore(current);
            SetHighScore(high);
            SetUpTweet(current);
        }

        private void SetCurrentScore(float value)
        {
            currentScore.text = $"{value.ToString("F2")}";
        }

        private void SetHighScore(float value)
        {
            highScore.text = $"{value.ToString("F2")}";
        }

        private void SetUpTweet(float value)
        {
            var tweetText = $"{value.ToString("F2")}秒で全問題をクリアした！\n";
            tweetText += $"#{GameConfig.GAME_ID}\n";
            tweetText += $"{UrlConfig.APP}";
            var url = $"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(tweetText)}";
            tweetButton.push
                .Subscribe(_ => Application.OpenURL(url))
                .AddTo(this);
        }
    }
}