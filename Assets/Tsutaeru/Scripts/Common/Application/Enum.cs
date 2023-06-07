namespace Tsutaeru.Common
{
    public enum SceneName
    {
        None,
        Boot,
        Main,
        Ranking,
    }

    public enum LoadType
    {
        None,
        Direct,
        Fade,
    }

    public enum BgmType
    {
        None,
        Title,
        Main,
        Result,
    }

    public enum SeType
    {
        None,
        Decision,
        Cancel,
        Transition,
        ProgressUp,
        Hint,
        Slide,
        Pop,
        Correct,
    }

    public enum GameMode
    {
        None,
        TimeAttack,
    }

    public enum ExceptionType
    {
        None,
        Cancel,
        Retry,
        Reboot,
        Crash,
    }
}