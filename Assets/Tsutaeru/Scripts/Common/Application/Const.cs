namespace Tsutaeru.Common
{
    public sealed class AppConfig
    {
        public const int MAJOR_VERSION = 1;
        public const int MINOR_VERSION = 0;
        public static readonly string APP_VERSION = $"{MAJOR_VERSION.ToString()}.{MINOR_VERSION.ToString()}";
    }

    public sealed class UiConfig
    {
        public const float ANIMATION_TIME = 0.5f;
        public const float POPUP_TIME = 0.25f;
    }

    public sealed class SceneConfig
    {
        public const float FADE_IN_TIME = 0.5f;
        public const float FADE_OUT_TIME = 1.5f;
    }

    public sealed class SoundConfig
    {
        public const float INIT_VOLUME = 5.0f;
        public const int MIN_VOLUME = 0;
        public const int MAX_VOLUME = 10;
    }

    public sealed class SaveKeyConfig
    {
        public const string ES3_KEY = "";
    }

    public sealed class PlayFabConfig
    {
        public const string TITLE_ID = "";
        public const string RANKING_TIME_ATTACK_KEY = "";
        public const string USER_TIME_ATTACK_KEY = "";
        public const string MASTER_APP_VERSION_KEY = "";
        public const int SCORE_RATE = 10000;
        public const int SHOW_MAX_RANKING = 100;
    }

    public sealed class ExceptionConfig
    {
        public const string NOT_FOUND_DATA = "";
        public const string NOT_FOUND_STATE = "";
        public const string NOT_FOUND_PROGRESS = "";
        public const string NOT_FOUND_PREFAB = "";

        public const string FAILED_DESERIALIZE_MASTER = "";
        public const string FAILED_RESPONSE_DATA = "";
        public const string FAILED_UPDATE_DATA = "";
        public const string FAILED_LOGIN = "";

        public const string UNMATCHED_GAME_MODE = "";
        public const string UNMATCHED_TYPE_LOAD = "";
        public const string UNMATCHED_TYPE_MOVE = "";
        public const string UNMATCHED_TYPE_HINT = "";
        public const string UNMATCHED_USER_NAME_RULE = "";
    }
}