using PlayFab.ClientModels;

namespace Tsutaeru.Common.Data.Entity
{
    public abstract class RankingRecordEntity
    {
        private readonly PlayerLeaderboardEntry _entry;

        protected RankingRecordEntity(PlayerLeaderboardEntry entry, string userId)
        {
            _entry = entry;
            isSelf = id.Equals(userId);
        }

        protected abstract GameMode mode { get; }
        public bool isSelf { get; }

        public string id => _entry.PlayFabId;
        public int rank => _entry.Position + 1;
        public string name => _entry.DisplayName;

        public virtual float GetScore()
        {
            return _entry.Profile.Statistics?.Find(x => x.Name == mode.ToRankingKey())?.Value ?? 0;
        }
    }
}