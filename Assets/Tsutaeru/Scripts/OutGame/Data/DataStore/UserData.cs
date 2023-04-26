using Tsutaeru.OutGame.Data.Entity;

namespace Tsutaeru.OutGame.Data.DataStore
{
    public sealed class UserData
    {
        public readonly UserEntity user;

        public UserData(string name, UserTimeAttackData timeAttackData)
        {
            user = new UserEntity();
            user.SetUserName(name);
            user.SetTimeAttack(timeAttackData.timeAttack);
        }
    }

    public sealed class UserTimeAttackData
    {
        public UserTimeAttackEntity timeAttack;

        public static UserTimeAttackData Default()
        {
            return new UserTimeAttackData
            {
                timeAttack = UserTimeAttackEntity.Default(),
            };
        }
    }
}