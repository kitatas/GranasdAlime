namespace Tsutaeru.Common.Data.Entity
{
    public sealed class UserEntity
    {
        public string userName { get; private set; }
        public UserTimeAttackEntity timeAttackEntity { get; private set; }

        public void Set(UserEntity entity)
        {
            SetUserName(entity.userName);
            SetTimeAttack(entity.timeAttackEntity);
        }

        public void SetUserName(string name)
        {
            userName = name;
        }

        public void SetTimeAttack(UserTimeAttackEntity timeAttack)
        {
            timeAttackEntity = timeAttack;
        }

        public bool IsEmptyUserName()
        {
            return string.IsNullOrEmpty(userName);
        }
    }
}