using System;

namespace Tsutaeru.InGame.Data.DataStore
{
    [Serializable]
    public sealed class QuestionTable
    {
        public QuestionData[] data_list;
    }

    [Serializable]
    public sealed class QuestionData
    {
        public int question_id;
        public Difficulty difficulty;
        public string origin;
        public HintType originHint;
        public string answer;
        public HintType answerHint;
    }
}