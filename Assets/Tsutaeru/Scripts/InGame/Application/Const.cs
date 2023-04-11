namespace Tsutaeru.InGame
{
    public sealed class GameConfig
    {
        public const string GAME_ID = "granasd_alime";

        public const GameState INIT_STATE = GameState.Title;

        public const int MAX_QUESTION = 20;
    }

    public sealed class ProgressConfig
    {
        public const int EASY = 8;
        public const int NORMAL = 15;
        public const int HARD = SPECIAL - 1;
        public const int SPECIAL = GameConfig.MAX_QUESTION;
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

    public sealed class ResourceConfig
    {
        public const string BASE_PATH = "Assets/Tsutaeru-Assets/";
        public const string JSON_PATH = BASE_PATH + "Master/Json/";

        public const string BASE_PATH2 = "Assets/Tsutaeru/";
        public const string TABLE_PATH = BASE_PATH2 + "Master/ScriptableObjects/";
        public const string SOUND_TABLE_PATH = TABLE_PATH + "Sound/";
    }
}