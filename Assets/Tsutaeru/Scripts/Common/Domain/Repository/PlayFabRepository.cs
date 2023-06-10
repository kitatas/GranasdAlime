using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using Tsutaeru.Common.Data.DataStore;
using UniEx;

namespace Tsutaeru.Common.Domain.Repository
{
    public sealed class PlayFabRepository
    {
        public PlayFabRepository()
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
                throw new RetryException(ExceptionConfig.FAILED_LOGIN);
            }

            return response.Result;
        }

        public UserData FetchUserData(LoginResult response)
        {
            if (response == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            var payload = response.InfoResultPayload;
            if (payload == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            var userDataRecord = payload.UserData;
            if (userDataRecord == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            var profile = payload.PlayerProfile;
            var userName = profile == null ? "" : profile.DisplayName;

            return new UserData(userName, userDataRecord);
        }

        public async UniTask<bool> UpdateUserNameAsync(string name, CancellationToken token)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new RetryException(ExceptionConfig.UNMATCHED_USER_NAME_RULE);
            }

            if (name.Length.IsBetween(3, 10) == false)
            {
                throw new RetryException(ExceptionConfig.UNMATCHED_USER_NAME_RULE);
            }

            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = name,
            };

            var response = await PlayFabClientAPI.UpdateUserTitleDisplayNameAsync(request);
            if (response.Error != null)
            {
                // 名前更新失敗の要因を2つに絞る
                // すでに登録されているユーザー名 or それ以外
                if (response.Error.Error == PlayFabErrorCode.NameNotAvailable)
                {
                    throw new RetryException(ExceptionConfig.UNMATCHED_USER_NAME_RULE);
                }
                else
                {
                    throw new RetryException(ExceptionConfig.FAILED_UPDATE_DATA);
                }
            }

            return true;
        }

        public async UniTask<MasterData> FetchMasterDataAsync(CancellationToken token)
        {
            var request = new GetTitleDataRequest();
            var response = await PlayFabClientAPI.GetTitleDataAsync(request);
            if (response.Error != null)
            {
                throw new RetryException(ExceptionConfig.FAILED_RESPONSE_DATA);
            }

            var result = response.Result;
            if (result == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            var data = result.Data;
            if (data == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            return new MasterData(data);
        }

        public async UniTask<UpdateUserDataResult> UpdateTimeAttackRecordAsync(Data.Entity.UserTimeAttackEntity timeAttackEntity, CancellationToken token)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { PlayFabConfig.USER_TIME_ATTACK_KEY, timeAttackEntity.ToJson() },
                },
            };

            var response = await PlayFabClientAPI.UpdateUserDataAsync(request);
            if (response.Error != null)
            {
                throw new RetryException(ExceptionConfig.FAILED_UPDATE_DATA);
            }

            return response.Result;
        }

        public async UniTask SendToTimeAttackRankingAsync(Data.Entity.UserTimeAttackEntity timeAttackEntity, CancellationToken token)
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = PlayFabConfig.RANKING_TIME_ATTACK_KEY,
                        Value = timeAttackEntity.GetCurrentForRanking(),
                    },
                },
            };

            var response = await PlayFabClientAPI.UpdatePlayerStatisticsAsync(request);
            if (response.Error != null)
            {
                throw new RetryException(ExceptionConfig.FAILED_UPDATE_DATA);
            }
        }

        public async UniTask<RankingRecordData> GetRankDataAsync(GameMode mode, CancellationToken token)
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = mode.ToRankingKey(),
                ProfileConstraints = new PlayerProfileViewConstraints
                {
                    ShowDisplayName = true,
                    ShowStatistics = true,
                },
                MaxResultsCount = PlayFabConfig.SHOW_MAX_RANKING,
            };

            var response = await PlayFabClientAPI.GetLeaderboardAsync(request);
            if (response.Error != null)
            {
                throw new RetryException(ExceptionConfig.FAILED_RESPONSE_DATA);
            }

            var result = response.Result;
            if (result == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            var leaderboard = result.Leaderboard;
            if (leaderboard == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            return new RankingRecordData(leaderboard, mode);
        }
    }
}