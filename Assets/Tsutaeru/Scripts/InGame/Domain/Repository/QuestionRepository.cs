using System;
using System.Collections.Generic;
using Tsutaeru.InGame.Data.DataStore;
using UniEx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tsutaeru.InGame.Domain.Repository
{
    public sealed class QuestionRepository
    {
        private List<QuestionData> _questionTable;

        public QuestionRepository()
        {
            Addressables.LoadAssetAsync<TextAsset>(GetKey("unity1week")).Completed += x =>
            {
                var table = JsonUtility.FromJson<QuestionTable>(x.Result.text);
                _questionTable = new List<QuestionData>(table.data_list);
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
        public QuestionData Find(Difficulty difficulty)
        {
            var target = _questionTable.FindAll(x => x.difficulty == difficulty);
            if (target.Count == 0)
            {
                throw new Exception($"target question is nothing. (difficulty: {difficulty})");
            }

            var data = target.GetRandom();
            if (data == null)
            {
                throw new Exception($"question data is null.");
            }

            if (string.IsNullOrEmpty(data.origin) || data.originHint == HintType.None)
            {
                throw new Exception($"question origin data is invalid. (name: {data.question_id})");
            }

            if (string.IsNullOrEmpty(data.answer) || data.answerHint == HintType.None)
            {
                throw new Exception($"question answer data is invalid. (name: {data.question_id})");
            }

            if (data.origin.Length != data.answer.Length)
            {
                throw new Exception($"question data is invalid. (name: {data.question_id})");
            }

            // 再抽選されないようにする
            _questionTable.Remove(data);

            return data;
        }
    }
}