using Tsutaeru.Base.Data.Entity;
using Tsutaeru.Common;

namespace Tsutaeru.InGame.Data.Entity
{
    public sealed class GameModeEntity : BaseEntity<GameMode>
    {
        public GameModeEntity()
        {
            Set(GameMode.None);
        }
    }
}