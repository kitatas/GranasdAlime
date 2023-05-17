using TMPro;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class RankingRecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rank = default;
        [SerializeField] private TextMeshProUGUI userName = default;
        [SerializeField] private TextMeshProUGUI score = default;

        public void SetData(Common.Data.Entity.RankingRecordEntity entity)
        {
            rank.text = $"{entity.rank.ToString()}";
            userName.text = $"{entity.name}";
            score.text = $"{entity.GetScore().ToString("F2")}";
        }
    }
}