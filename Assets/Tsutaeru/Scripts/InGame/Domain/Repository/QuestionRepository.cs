using System.Collections.Generic;
using Tsutaeru.InGame.Data.DataStore;
using UniEx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tsutaeru.InGame.Domain.Repository
{
    public sealed class QuestionRepository
    {
        private List<Data.Entity.QuestionEntity> _questionTable;

        public QuestionRepository()
        {
            Addressables.LoadAssetAsync<TextAsset>(GetKey("unity1week")).Completed += x =>
            {
                var table = JsonUtility.FromJson<QuestionTable>(x.Result.text);
                _questionTable = new List<Data.Entity.QuestionEntity>(table.data_list);
            };
        }

        private static string GetKey(string jsonName)
        {
            return $"{ResourceConfig.JSON_PATH}{jsonName}.json";
        }

        /// <summary>
        /// 指定した難易度から対象の問題を1つ取得する
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public Data.Entity.QuestionEntity Find(Difficulty difficulty)
        {
            var data = _questionTable
                .FindAll(x => x.difficulty == difficulty)
                .GetRandom();

            // 再抽選されないようにする
            _questionTable.Remove(data);

            return data;
        }
    }
}