using System;

namespace Tsutaeru.Common
{
    public static class CustomExtension
    {
        public static string ToRankingKey(this RankingType type)
        {
            return type switch
            {
                RankingType.TimeAttack => PlayFabConfig.RANKING_TIME_ATTACK_KEY,
                _ => throw new Exception(ExceptionConfig.UNMATCHED_TYPE_RANKING)
            };
        }
    }
}