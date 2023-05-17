using PlayFab.ClientModels;

namespace Tsutaeru.Common.Data.Entity
{
    public sealed class TimeAttackRecordEntity : RankingRecordEntity
    {
        public TimeAttackRecordEntity(PlayerLeaderboardEntry entry) : base(entry)
        {
        }

        protected override RankingType type => RankingType.TimeAttack;

        public override float GetScore()
        {
            return base.GetScore() / PlayFabConfig.SCORE_RATE;
        }
    }
}