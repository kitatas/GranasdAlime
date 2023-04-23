using System.Threading;
using Cysharp.Threading.Tasks;
using PlayFab.ClientModels;
using Tsutaeru.OutGame.Domain.Repository;

namespace Tsutaeru.Boot.Domain.UseCase
{
    public sealed class LoginUseCase
    {
        private readonly BackendRepository _backendRepository;
        private readonly SaveRepository _saveRepository;

        public LoginUseCase(BackendRepository backendRepository, SaveRepository saveRepository)
        {
            _backendRepository = backendRepository;
            _saveRepository = saveRepository;
        }

        public async UniTask<bool> LoginAsync(CancellationToken token)
        {
            var response = await LoginOrCreateAsync(token);
            var userData = _backendRepository.FetchUserData(response);

            // TODO: check user name

            return true;
        }

        private async UniTask<LoginResult> LoginOrCreateAsync(CancellationToken token)
        {
            var saveData = _saveRepository.Load();

            // 新規ユーザー
            if (string.IsNullOrEmpty(saveData.uid))
            {
                var (uid, response) = await _backendRepository.CreateUserAsync(token);
                _saveRepository.SaveUid(uid);
                return response;
            }
            else
            {
                return await _backendRepository.LoginUserAsync(saveData.uid, token);
            }
        }
    }
}