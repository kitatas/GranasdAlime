using System;
using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Tsutaeru.Common;
using Tsutaeru.Common.Data.DataStore;
using Tsutaeru.InGame;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.TestTools;

namespace Tsutaeru.Tests.Master
{
    public sealed class Sound
    {
        [UnityTest]
        public IEnumerator BgmWithEnumeratorPasses() => UniTask.ToCoroutine(async () =>
        {
            // bgm master
            try
            {
                var filePath = $"{ResourceConfig.SOUND_TABLE_PATH}Bgm/BgmTable.asset";
                var table = await Addressables.LoadAssetAsync<BgmTable>(filePath);
                Assert.IsNotNull(table, $"bgm table is null.");

                var records = table.data;
                Assert.IsNotNull(records, $"bgm records is null.");
                Assert.IsTrue(records.Count > 0, $"bgm records is nothing.");

                foreach (var record in table.data)
                {
                    // record
                    Assert.IsNotNull(record, $"bgm record is null.");

                    // pk
                    var type = record.type;
                    Assert.IsTrue(type != BgmType.None, $"bgm type is invalid: {type}");

                    var isUniq = records.Count(x => x.type == type) == 1;
                    Assert.IsTrue(isUniq, $"bgm type is not uniq: {type}");

                    // clip
                    Assert.IsNotNull(record.clip, $"bmg clip is null: {type}");
                }

                foreach (var type in Enum.GetValues(typeof(BgmType)).Cast<BgmType>())
                {
                    if (type == BgmType.None) continue;
                    var isExist = records.Count(x => x.type == type) == 1;
                    Assert.IsTrue(isExist, $"bgm type's clip is nothing: {type}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"bgm master error: {e}");
                throw;
            }
        });

        [UnityTest]
        public IEnumerator SeWithEnumeratorPasses() => UniTask.ToCoroutine(async () =>
        {
            // se master
            try
            {
                var filePath = $"{ResourceConfig.SOUND_TABLE_PATH}Se/SeTable.asset";
                var table = await Addressables.LoadAssetAsync<SeTable>(filePath);
                Assert.IsNotNull(table, $"se table is null.");

                var records = table.data;
                Assert.IsNotNull(records, $"se records is null.");
                Assert.IsTrue(records.Count > 0, $"se records is nothing.");

                foreach (var record in table.data)
                {
                    // record
                    Assert.IsNotNull(record, $"se record is null.");

                    // pk
                    var type = record.type;
                    Assert.IsTrue(type != SeType.None, $"se type is invalid: {type}");

                    var isUniq = records.Count(x => x.type == type) == 1;
                    Assert.IsTrue(isUniq, $"se type is not uniq: {type}");

                    // clip
                    Assert.IsNotNull(record.clip, $"se clip is null: {type}");
                }

                foreach (var type in Enum.GetValues(typeof(SeType)).Cast<SeType>())
                {
                    if (type == SeType.None) continue;
                    var isExist = records.Count(x => x.type == type) == 1;
                    Assert.IsTrue(isExist, $"se type's clip is nothing: {type}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"se master error: {e}");
                throw;
            }
        });
    }
}