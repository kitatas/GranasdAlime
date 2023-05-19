using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Data.Entity;
using Tsutaeru.Common.Domain.Repository;
using Tsutaeru.InGame.Data.Entity;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class UserRecordUseCase
    {
        private readonly TimeEntity _timeEntity;
        private readonly UserEntity _userEntity;
        private readonly PlayFabRepository _playFabRepository;

        public UserRecordUseCase(TimeEntity timeEntity, UserEntity userEntity, PlayFabRepository playFabRepository)
        {
            _timeEntity = timeEntity;
            _userEntity = userEntity;
            _playFabRepository = playFabRepository;
        }

        public async UniTask SendTimeAttackScoreAsync(CancellationToken token)
        {
            var timeAttackEntity = _userEntity.timeAttackEntity.UpdateByPlay(_timeEntity.value);
            await UniTask.WhenAll(
                _playFabRepository.UpdateTimeAttackRecordAsync(timeAttackEntity, token),
                _playFabRepository.SendToTimeAttackRankingAsync(timeAttackEntity, token)
            );

            _userEntity.SetTimeAttack(timeAttackEntity);
        }

        public (float current, float high) GetUserScore()
        {
            return (
                current: _userEntity.timeAttackEntity.current,
                high: _userEntity.timeAttackEntity.high
            );
        }
    }
}