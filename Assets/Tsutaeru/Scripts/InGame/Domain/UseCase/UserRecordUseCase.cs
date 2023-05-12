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
            var entity = _userEntity.timeAttackEntity.UpdateByPlay(_timeEntity.value);
            await (
                _playFabRepository.UpdateTimeAttackRecordAsync(entity, token)
            );
        }
    }
}