namespace Tsutaeru.InGame
{
    public enum GameState
    {
        None,
        SetUp,
        Input,
        Judge,
        Clear,
        Result,
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
        Food,
        Fluid,
        Sport,
        Item,
        Country,
        Action,
        Service,
        Metal,
        Creature,
        Grain,
        Data,
        Human,
        Vehicle,
        Rule,
        Title,
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