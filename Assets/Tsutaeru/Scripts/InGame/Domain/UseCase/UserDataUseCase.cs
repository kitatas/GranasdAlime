using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Data.Entity;
using Tsutaeru.Common.Domain.Repository;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class UserDataUseCase
    {
        private readonly UserEntity _userEntity;
        private readonly BackendRepository _backendRepository;
        private readonly SaveRepository _saveRepository;

        public UserDataUseCase(UserEntity userEntity, BackendRepository backendRepository, SaveRepository saveRepository)
        {
            _userEntity = userEntity;
            _backendRepository = backendRepository;
            _saveRepository = saveRepository;
        }

        public string GetUserName()
        {
            return _userEntity.userName;
        }

        public async UniTask<bool> UpdateUserNameAsync(string name, CancellationToken token)
        {
            var isSuccess = await _backendRepository.UpdateUserNameAsync(name, token);
            if (isSuccess)
            {
                _userEntity.SetUserName(name);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete()
        {
            _saveRepository.Delete();
        }
    }
}