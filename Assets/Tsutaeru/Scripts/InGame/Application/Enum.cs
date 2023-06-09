namespace Tsutaeru.InGame
{
    public enum GameState
    {
        None,
        Title,
        TaSetUp,
        TaInput,
        TaJudge,
        TaClear,
        TaFinish,
        TaResult,
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
        None = 0,
        Drink = 1,
        Jewel = 2,
        Food = 3,
        Fluid = 4,
        Sport = 5,
        Item = 6,
        Country = 7,
        Action = 8,
        Service = 9,
        Metal = 10,
        Creature = 11,
        Object = 12,
        Data = 13,
        Human = 14,
        Vehicle = 15,
        Rule = 16,
        Title = 17,
        Unit = 18,
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