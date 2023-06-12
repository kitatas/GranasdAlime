using System.Threading;
using Cysharp.Threading.Tasks;
using PlayFab.ClientModels;
using Tsutaeru.Common.Data.Entity;
using Tsutaeru.Common.Domain.Repository;

namespace Tsutaeru.Boot.Domain.UseCase
{
    public sealed class LoginUseCase
    {
        private readonly UserEntity _userEntity;
        private readonly PlayFabRepository _playFabRepository;
        private readonly SaveRepository _saveRepository;

        public LoginUseCase(UserEntity userEntity, PlayFabRepository playFabRepository, SaveRepository saveRepository)
        {
            _userEntity = userEntity;
            _playFabRepository = playFabRepository;
            _saveRepository = saveRepository;
        }

        public async UniTask<bool> LoginAsync(CancellationToken token)
        {
            var response = await LoginOrCreateAsync(token);
            var userData = _playFabRepository.FetchUserData(response);
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
                var (uid, response) = await _playFabRepository.CreateUserAsync(token);
                _saveRepository.SaveUid(uid);
                return response;
            }
            else
            {
                return await _playFabRepository.LoginUserAsync(saveData.uid, token);
            }
        }

        public async UniTask<bool> RegisterAsync(string name, CancellationToken token)
        {
            var isSuccess = await _playFabRepository.UpdateUserNameAsync(name, token);
            if (isSuccess)
            {
                // 再ログインでユーザーデータをキャッシュする
                await LoginAsync(token);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}