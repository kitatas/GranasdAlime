using System;

namespace Tsutaeru.Common
{
    public static class CustomExtension
    {
        public static string ToRankingKey(this GameMode mode)
        {
            return mode switch
            {
                GameMode.TimeAttack => PlayFabConfig.RANKING_TIME_ATTACK_KEY,
                _ => throw new Exception(ExceptionConfig.UNMATCHED_GAME_MODE)
            };
        }
    }
}