using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using Tsutaeru.OutGame.Data.DataStore;

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

            var userData = payload.UserData;
            if (userData == null)
            {
                throw new Exception($"login result user data is null.");
            }

            return userData.TryGetValue(PlayFabConfig.USER_KEY, out var user)
                ? JsonConvert.DeserializeObject<UserData>(user.Value)
                : UserData.Default();
        }
    }
}