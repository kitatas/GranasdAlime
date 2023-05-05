using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using Tsutaeru.OutGame.Data.DataStore;
using UniEx;

namespace Tsutaeru.OutGame.Domain.Repository
{
    public sealed class BackendRepository
    {
        public BackendRepository()
        {
            PlayFabSettings.staticSettings.TitleId = PlayFabConfig.TITLE_ID;
        }

        public async UniTask<(string, LoginResult)> CreateUserAsync(CancellationToken token)
        {
            while (true)
            {
                var uid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
                var response = await LoginUserAsync(uid, token);

                // 新規作成できなければ、uidを再生成する
                if (response.NewlyCreated)
                {
                    return (uid, response);
                }
            }
        }

        public async UniTask<LoginResult> LoginUserAsync(string uid, CancellationToken token)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = uid,
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserData = true,
                    GetPlayerProfile = true,
                },
            };

            var response = await PlayFabClientAPI.LoginWithCustomIDAsync(request);
            if (response.Error != null)
            {
                throw new Exception($"login failed: {response.Error}");
            }

            return response.Result;
        }

        public UserData FetchUserData(LoginResult response)
        {
            if (response == null)
            {
                throw new Exception($"login result is null.");
            }

            var payload = response.InfoResultPayload;
            if (payload == null)
            {
                throw new Exception($"login result payload is null.");
            }

            var profile = payload.PlayerProfile;
            if (profile == null)
            {
                throw new Exception($"login result profile is null.");
            }

            var userDataRecord = payload.UserData;
            if (userDataRecord == null)
            {
                throw new Exception($"login result user data is null.");
            }

            var userName = profile.DisplayName;
            var timeAttackData = userDataRecord.TryGetValue(PlayFabConfig.USER_TIME_ATTACK_KEY, out var user)
                ? JsonConvert.DeserializeObject<UserTimeAttackData>(user.Value)
                : UserTimeAttackData.Default();

            return new UserData(userName, timeAttackData);
        }

        public async UniTask<bool> UpdateUserNameAsync(string name, CancellationToken token)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"invalid name: null or empty or white space.");
            }

            if (name.Length.IsBetween(3, 10) == false)
            {
                throw new Exception($"invalid name length: ${name.Length}");
            }

            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = name,
            };

            var response = await PlayFabClientAPI.UpdateUserTitleDisplayNameAsync(request);
            if (response.Error != null)
            {
                throw new Exception($"update name failed: {response.Error}");
            }

            return true;
        }

        public async UniTask<MasterData> FetchMasterDataAsync(CancellationToken token)
        {
            var request = new GetTitleDataRequest();
            var response = await PlayFabClientAPI.GetTitleDataAsync(request);
            if (response.Error != null)
            {
                throw new Exception($"get title data failed: {response.Error}");
            }

            var result = response.Result;
            if (result == null)
            {
                throw new Exception($"title data result is null.");
            }

            var data = result.Data;
            if (data == null)
            {
                throw new Exception($"title data is null.");
            }

            return new MasterData(data);
        }
    }
}