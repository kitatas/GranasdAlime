using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Domain.Repository;

namespace Tsutaeru.Boot.Domain.UseCase
{
    public sealed class AppVersionUseCase
    {
        private readonly PlayFabRepository _playFabRepository;

        public AppVersionUseCase(PlayFabRepository playFabRepository)
        {
            _playFabRepository = playFabRepository;
        }

        public async UniTask<bool> CheckUpdateAsync(CancellationToken token)
        {
            var masterData = await _playFabRepository.FetchMasterDataAsync(token);
            var appVersion = masterData.GetAppVersion();
            return appVersion.IsForceUpdate();
        }
    }
}