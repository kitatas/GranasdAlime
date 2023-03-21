using Tsutaeru.InGame.Data.Entity;
using Tsutaeru.InGame.Domain.Repository;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class QuestionUseCase
    {
        private readonly ProgressEntity _progressEntity;
        private readonly QuestionRepository _questionRepository;

        public QuestionUseCase(ProgressEntity progressEntity, QuestionRepository questionRepository)
        {
            _progressEntity = progressEntity;
            _questionRepository = questionRepository;
        }

        public Data.DataStore.QuestionData Lot()
        {
            // TODO: 問題クリア数から難易度を取得する
            var difficulty = Difficulty.Easy;
            return _questionRepository.Find(difficulty);
        }

        public bool IsAllClear()
        {
            _progressEntity.Increase();
            return _progressEntity.IsCount(GameConfig.MAX_QUESTION);
        }
    }
}