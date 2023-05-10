using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Domain.Repository;

namespace Tsutaeru.Boot.Domain.UseCase
{
    public sealed class AppVersionUseCase
    {
        private readonly BackendRepository _backendRepository;

        public AppVersionUseCase(BackendRepository backendRepository)
        {
            _backendRepository = backendRepository;
        }

        public async UniTask<bool> CheckUpdateAsync(CancellationToken token)
        {
            var masterData = await _backendRepository.FetchMasterDataAsync(token);
            var appVersion = masterData.GetAppVersion();
            return appVersion.IsForceUpdate();
        }
    }
}