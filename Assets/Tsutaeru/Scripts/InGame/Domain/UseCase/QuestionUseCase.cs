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

        public QuestionEntity Lot()
        {
            var difficulty = _progressEntity.GetDifficulty();
            return _questionRepository.Find(difficulty);
        }

        public int progress => _progressEntity.value + 1;

        public void IncreaseProgress()
        {
            _progressEntity.Increase();
        }

        public bool IsAllClear()
        {
            return _progressEntity.IsCount(GameConfig.MAX_QUESTION);
        }
    }
}