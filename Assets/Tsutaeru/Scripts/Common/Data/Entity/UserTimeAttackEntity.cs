namespace Tsutaeru.Common.Data.Entity
{
    public sealed class UserTimeAttackEntity
    {
        public float current { get; private set; }
        public float high { get; private set; }
        public int playCount { get; private set; }

        public static UserTimeAttackEntity Default()
        {
            return new UserTimeAttackEntity
            {
                current = 0.0f,
                high = 0.0f,
                playCount = 0,
            };
        }

        public UserTimeAttackEntity UpdateByPlay(float score)
        {
            return new UserTimeAttackEntity
            {
                current = score,
                high = score > high ? score : high,
                playCount = playCount + 1,
            };
        }
    }
}