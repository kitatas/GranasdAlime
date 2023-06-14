using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Tsutaeru.Common.Data.Entity;

namespace Tsutaeru.Common.Data.DataStore
{
    public sealed class MasterData
    {
        private readonly Dictionary<string, string> _resultData;

        public MasterData(Dictionary<string, string> resultData)
        {
            _resultData = resultData;
        }

        public AppVersionEntity GetAppVersion()
        {
            return _resultData.TryGetValue(PlayFabConfig.MASTER_APP_VERSION_KEY, out var json)
                ? JsonConvert.DeserializeObject<AppVersionEntity>(json)
                : throw new CrashException(ExceptionConfig.FAILED_DESERIALIZE_MASTER);
        }
    }
}