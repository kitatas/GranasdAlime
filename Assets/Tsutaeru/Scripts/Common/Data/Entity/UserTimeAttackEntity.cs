using Newtonsoft.Json;

namespace Tsutaeru.Common.Data.Entity
{
    public sealed class UserTimeAttackEntity
    {
        public float current;
        public float high;
        public int playCount;

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
                high = score < high ? score : high,
                playCount = playCount + 1,
            };
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public int GetCurrentForRanking()
        {
            return (int)(current * PlayFabConfig.SCORE_RATE);
        }
    }
}