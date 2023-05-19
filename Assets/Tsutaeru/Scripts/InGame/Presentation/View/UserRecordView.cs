using TMPro;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class UserRecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentScore = default;
        [SerializeField] private TextMeshProUGUI highScore = default;

        public void SetScore(float current, float high)
        {
            SetCurrentScore(current);
            SetHighScore(high);
        }

        private void SetCurrentScore(float value)
        {
            currentScore.text = $"{value.ToString("F2")}";
        }

        private void SetHighScore(float value)
        {
            highScore.text = $"{value.ToString("F2")}";
        }
    }
}