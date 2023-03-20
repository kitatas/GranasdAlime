using Tsutaeru.InGame.Domain.Repository;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class QuestionUseCase
    {
        private readonly QuestionRepository _questionRepository;

        public QuestionUseCase(QuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public Data.DataStore.QuestionData Lot()
        {
            // TODO: 問題クリア数から難易度を取得する
            var difficulty = Difficulty.Easy;
            return _questionRepository.Find(difficulty);
        }
    }
}