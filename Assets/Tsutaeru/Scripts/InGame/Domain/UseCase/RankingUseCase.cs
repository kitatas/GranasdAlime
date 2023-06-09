using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;
using Tsutaeru.Common.Data.Entity;
using Tsutaeru.Common.Domain.Repository;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class RankingUseCase
    {
        private readonly UserEntity _userEntity;
        private readonly PlayFabRepository _playFabRepository;

        public RankingUseCase(UserEntity userEntity, PlayFabRepository playFabRepository)
        {
            _userEntity = userEntity;
            _playFabRepository = playFabRepository;
        }

        public async UniTask<List<TimeAttackRecordEntity>> GetTimeAttackRankingAsync(CancellationToken token)
        {
            var recordData = await _playFabRepository.GetRankDataAsync(GameMode.TimeAttack, token);
            return recordData.GetTimeAttackRanking(_userEntity.userId);
        }
    }
}