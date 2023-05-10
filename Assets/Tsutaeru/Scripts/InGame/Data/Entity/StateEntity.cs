using Tsutaeru.Base.Data.Entity;

namespace Tsutaeru.InGame.Data.Entity
{
    public sealed class StateEntity : BaseEntity<GameState>
    {
        public bool IsState(GameState state)
        {
            return value == state;
        }
    }
}