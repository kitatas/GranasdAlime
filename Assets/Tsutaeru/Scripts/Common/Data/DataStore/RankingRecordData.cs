using System;
using System.Collections.Generic;
using System.Linq;
using PlayFab.ClientModels;
using Tsutaeru.Common.Data.Entity;

namespace Tsutaeru.Common.Data.DataStore
{
    public sealed class RankingRecordData
    {
        private readonly List<PlayerLeaderboardEntry> _leaderboard;
        private readonly RankingType _type;

        public RankingRecordData(List<PlayerLeaderboardEntry> leaderboard, RankingType type)
        {
            _leaderboard = leaderboard;
            _type = type;
        }

        public List<TimeAttackRecordEntity> GetTimeAttackRanking()
        {
            if (_type != RankingType.TimeAttack)
            {
                throw new Exception($"unmatched ranking type: {_type}");
            }

            return _leaderboard
                .Select(x => new TimeAttackRecordEntity(x))
                .ToList();
        }
    }
}