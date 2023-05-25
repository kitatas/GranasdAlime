namespace Tsutaeru.Common
{
    public sealed class AppConfig
    {
        public const int MAJOR_VERSION = 1;
        public const int MINOR_VERSION = 0;
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
}