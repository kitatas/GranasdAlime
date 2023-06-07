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
        private readonly GameMode _mode;

        public RankingRecordData(List<PlayerLeaderboardEntry> leaderboard, GameMode mode)
        {
            _leaderboard = leaderboard;
            _mode = mode;
        }

        public List<TimeAttackRecordEntity> GetTimeAttackRanking()
        {
            if (_mode != GameMode.TimeAttack)
            {
                throw new RebootException(ExceptionConfig.UNMATCHED_GAME_MODE);
            }

            return _leaderboard
                .Select(x => new TimeAttackRecordEntity(x))
                .ToList();
        }
    }
}