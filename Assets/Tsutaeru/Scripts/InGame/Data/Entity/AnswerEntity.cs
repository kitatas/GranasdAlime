namespace Tsutaeru.InGame.Data.Entity
{
    public sealed class AnswerEntity
    {
        private readonly string _answer;

        public AnswerEntity(DataStore.QuestionData data)
        {
            _answer = data.answer;
        }

        public bool IsCorrect(string userAnswer)
        {
            return _answer == userAnswer;
        }
    }
}