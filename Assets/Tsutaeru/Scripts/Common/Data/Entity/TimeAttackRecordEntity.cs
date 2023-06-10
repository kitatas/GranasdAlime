using PlayFab.ClientModels;

namespace Tsutaeru.Common.Data.Entity
{
    public sealed class TimeAttackRecordEntity : RankingRecordEntity
    {
        public TimeAttackRecordEntity(PlayerLeaderboardEntry entry, string userId) : base(entry, userId)
        {
        }

        protected override GameMode mode => GameMode.TimeAttack;

        public override float GetScore()
        {
            return (base.GetScore() / PlayFabConfig.SCORE_RATE) * -1;
        }
    }
}