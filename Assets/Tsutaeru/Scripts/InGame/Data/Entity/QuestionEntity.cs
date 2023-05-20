using System;

namespace Tsutaeru.InGame.Data.Entity
{
    [Serializable]
    public sealed class QuestionEntity
    {
        public int question_id;
        public Difficulty difficulty;
        public string origin;
        public HintType originHint;
        public string answer;
        public HintType answerHint;

        public int questionLength => origin.Length;

        public char GetQuestionChar(int index)
        {
            return origin[index];
        }

        public bool IsCorrectAnswer(string userAnswer)
        {
            return answer == userAnswer;
        }

        public string GetHintMessage()
        {
            return $"{originHint.ToWord()} <size=-10>カラ<size=+0> {answerHint.ToWord()} <size=-10>ヘ";
        }
    }
}