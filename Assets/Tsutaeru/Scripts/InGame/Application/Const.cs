namespace Tsutaeru.InGame
{
    public sealed class GameConfig
    {
        // TODO: fix init state
        public const GameState INIT_STATE = GameState.SetUp;

        public const int MAX_QUESTION = 10;
    }

    public sealed class ProgressConfig
    {
        public const int EASY = 3;
        public const int NORMAL = 6;
        public const int HARD = 9;
        public const int SPECIAL = 10;
    }

    public sealed class WordConfig
    {
        public const int INTERVAL = 90;
        public const int SHIFT_RANGE = WordConfig.INTERVAL - 15;
        public const float FOCUS_SPEED = 0.05f;
        public const float SHIFT_SPEED = 0.1f;
    }
}