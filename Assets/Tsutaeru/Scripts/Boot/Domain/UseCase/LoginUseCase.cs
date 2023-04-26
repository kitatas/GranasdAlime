using System.Threading;
using Cysharp.Threading.Tasks;
using PlayFab.ClientModels;
using Tsutaeru.OutGame.Data.Entity;
using Tsutaeru.OutGame.Domain.Repository;

namespace Tsutaeru.Boot.Domain.UseCase
{
    public sealed class LoginUseCase
    {
        private readonly UserEntity _userEntity;
        private readonly BackendRepository _backendRepository;
        private readonly SaveRepository _saveRepository;

        public LoginUseCase(UserEntity userEntity, BackendRepository backendRepository, SaveRepository saveRepository)
        {
            _userEntity = userEntity;
            _backendRepository = backendRepository;
            _saveRepository = saveRepository;
        }

        public async UniTask<bool> LoginAsync(CancellationToken token)
        {
            var response = await LoginOrCreateAsync(token);
            var userData = _backendRepository.FetchUserData(response);
            _userEntity.Set(userData.user);

            // 名前が空であれば未登録ユーザー
            return _userEntity.IsEmptyUserName() == false;
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