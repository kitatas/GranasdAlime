namespace Tsutaeru.InGame
{
    public sealed class GameConfig
    {
        public const GameState INIT_STATE = GameState.Title;

        public const int MAX_QUESTION = 10;
    }

    public sealed class ProgressConfig
    {
        public const int EASY = 3;
        public const int NORMAL = 6;
        public const int HARD = 9;
        public const int SPECIAL = 10;
    }

    public sealed class UiConfig
    {
        public const float ANIMATION_TIME = 0.5f;
        public const float POPUP_TIME = 0.25f;
    }

    public sealed class WordConfig
    {
        public const int INTERVAL = 90;
        public const int SHIFT_RANGE = WordConfig.INTERVAL - 15;
        public const float FOCUS_SPEED = 0.05f;
        public const float SHIFT_SPEED = 0.1f;
        public const float GENERATE_SPEED = 0.25f;
    }
}