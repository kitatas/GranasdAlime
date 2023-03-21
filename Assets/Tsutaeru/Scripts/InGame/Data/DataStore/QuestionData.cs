using UnityEngine;

namespace Tsutaeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(QuestionData), menuName = "DataTable/" + nameof(QuestionData))]
    public sealed class QuestionData : ScriptableObject
    {
        [SerializeField] private Difficulty difficultyType = default;

        [SerializeField] private string originMessage = default;
        [SerializeField] private HintType originHintType = default;

        [SerializeField] private string answerMessage = default;
        [SerializeField] private HintType answerHintType = default;

        public Difficulty difficulty => difficultyType;
        public string origin => originMessage;
        public HintType originHint => originHintType;
        public string answer => answerMessage;
        public HintType answerHint => answerHintType;
    }
}