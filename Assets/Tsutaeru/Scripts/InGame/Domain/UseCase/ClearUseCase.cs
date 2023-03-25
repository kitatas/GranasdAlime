using Tsutaeru.InGame.Data.Entity;
using UniEx;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class ClearUseCase
    {
        private readonly ProgressEntity _progressEntity;

        private readonly string[] _clearMessages = new[]
        {
            "Amazing...!",
            "Awesome...!",
            "Excellent...!",
            "Fabulous...!",
            "Fantastic...!",
            "Genius...!",
            "Great...!",
            "Marvelous...!",
            "Perfect...!",
            "Wonderful...!",
        };

        private readonly string _lastMessage = "Congratulation...!";

        public ClearUseCase(ProgressEntity progressEntity)
        {
            _progressEntity = progressEntity;
        }

        public string GetClearMessage()
        {
            if (_progressEntity.IsCount(GameConfig.MAX_QUESTION))
            {
                return $"<size=+5>{_lastMessage}";
            }

            return $"<size=+5>{_clearMessages.GetRandom()}";
        }
    }
}