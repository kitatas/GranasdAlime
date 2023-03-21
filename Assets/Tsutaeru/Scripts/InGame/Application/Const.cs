namespace Tsutaeru.InGame
{
    public sealed class GameConfig
    {
        // TODO: fix init state
        public const GameState INIT_STATE = GameState.SetUp;
    }
    
    public sealed class WordConfig
    {
        public const int INTERVAL = 90;
        public const int SHIFT_RANGE = WordConfig.INTERVAL - 15;
    }
}