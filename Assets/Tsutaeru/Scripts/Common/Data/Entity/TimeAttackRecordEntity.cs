using PlayFab.ClientModels;

namespace Tsutaeru.Common.Data.Entity
{
    public sealed class TimeAttackRecordEntity
    {
        public readonly string id;
        public readonly int rank;
        public readonly string name;
        public readonly float score;

        public TimeAttackRecordEntity(PlayerLeaderboardEntry entry, RankingType type)
        {
            id = entry.PlayFabId;
            rank = entry.Position + 1;
            name = entry.DisplayName;

            float rankingScore = entry.Profile.Statistics?.Find(x => x.Name == type.ToRankingKey())?.Value ?? 0;
            score = rankingScore / PlayFabConfig.SCORE_RATE;
        }
    }
}