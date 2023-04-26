namespace Tsutaeru.OutGame.Data.Entity
{
    public sealed class UserEntity
    {
        public string userName { get; private set; }
        public UserTimeAttackEntity timeAttackEntity { get; private set; }

        public void SetUserName(string name)
        {
            userName = name;
        }

        public void SetTimeAttack(UserTimeAttackEntity timeAttack)
        {
            timeAttackEntity = timeAttack;
        }
    }
}