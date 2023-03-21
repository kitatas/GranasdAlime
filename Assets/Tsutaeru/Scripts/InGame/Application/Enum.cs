namespace Tsutaeru.InGame
{
    public enum GameState
    {
        None,
        SetUp,
        Input,
        Judge,
        Clear,
    }

    public enum Difficulty
    {
        None,
        Easy,
        Normal,
        Hard,
        Special,
    }

    public enum HintType
    {
        None,
        Drink,
        Jewel,
    }

    public enum WordStatus
    {
        None,
        First,
        Middle,
        Last,
    }

    public enum MoveStatus
    {
        None,
        Left,
        Right,
    }
}