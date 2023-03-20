using UnityEngine;

namespace Tsutaeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(QuestionData), menuName = "DataTable/" + nameof(QuestionData))]
    public sealed class QuestionData : ScriptableObject
    {
        [SerializeField] private Difficulty difficultyType = default;

        [SerializeField] private string originMessage = default;
        [SerializeField] private string originHintType = default;

        [SerializeField] private string answerMessage = default;
        [SerializeField] private string answerHintType = default;

        public Difficulty difficulty => difficultyType;
        public string origin => originMessage;
        public string originHint => originHintType;
        public string answer => answerMessage;
        public string answerHint => answerHintType;
    }
}