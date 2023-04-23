using Tsutaeru.OutGame.Data.Entity;

namespace Tsutaeru.OutGame.Data.DataStore
{
    public sealed class UserData
    {
        public UserTimeAttackEntity timeAttack;

        public static UserData Default()
        {
            return new UserData
            {
                timeAttack = UserTimeAttackEntity.Default()
            };
        }
    }
}