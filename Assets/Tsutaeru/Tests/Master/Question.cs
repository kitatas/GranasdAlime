using System;
using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Tsutaeru.InGame;
using Tsutaeru.InGame.Data.DataStore;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.TestTools;

namespace Tsutaeru.Tests.Master
{
    public sealed class Question
    {
        [UnityTest]
        public IEnumerator QuestionWithEnumeratorPasses() => UniTask.ToCoroutine(async () =>
        {
            // unity1week's master
            try
            {
                var filePath = $"{ResourceConfig.JSON_PATH}unity1week.json";
                var master = await Addressables.LoadAssetAsync<TextAsset>(filePath);
                Assert.IsNotNull(master, $"master file is null.");

                var table = JsonUtility.FromJson<QuestionTable>(master.text);
                Assert.IsNotNull(table, $"table is null.");

                // question count
                var records = table.data_list;
                Assert.IsNotNull(records, $"records is null.");
                Assert.IsTrue(records.Length == GameConfig.MAX_QUESTION, $"record count is not ok.");

                // question count for difficulty
                var easy = records.Count(x => x.difficulty == Difficulty.Easy);
                var normal = records.Count(x => x.difficulty == Difficulty.Normal);
                var hard = records.Count(x => x.difficulty == Difficulty.Hard);
                var special = records.Count(x => x.difficulty == Difficulty.Special);
                Assert.IsTrue(easy == ProgressConfig.EASY, $"easy count is not ok.");
                Assert.IsTrue(normal == ProgressConfig.NORMAL - ProgressConfig.EASY, $"normal count is not ok.");
                Assert.IsTrue(hard == ProgressConfig.HARD - ProgressConfig.NORMAL, $"hard count is not ok.");
                Assert.IsTrue(special == ProgressConfig.SPECIAL - ProgressConfig.HARD, $"special count is not ok.");

                foreach (var record in records)
                {
                    // record
                    Assert.IsNotNull(record, $"record is null.");

                    // pk
                    var qid = record.question_id;
                    var isUniq = records.Count(x => x.question_id == qid) == 1;
                    Assert.IsTrue(isUniq, $"qid is not uniq: {qid}");

                    // difficulty
                    Assert.IsTrue(record.difficulty != Difficulty.None, $"difficulty is invalid: {qid}");

                    // hint
                    var originHint = record.originHint;
                    var answerHint = record.answerHint;
                    Assert.IsTrue(originHint != HintType.None, $"origin hint is invalid: {qid}");
                    Assert.IsTrue(answerHint != HintType.None, $"answer hint is invalid: {qid}");
                    Assert.IsFalse(originHint.ToWord().IsNullOrEmpty(), $"origin hint word is invalid: {originHint}");
                    Assert.IsFalse(answerHint.ToWord().IsNullOrEmpty(), $"answer hint word is invalid: {answerHint}");

                    // origin & answer word
                    var origin = record.origin;
                    var answer = record.answer;
                    Assert.IsFalse(origin.IsNullOrEmpty(), $"origin is invalid: {qid}");
                    Assert.IsFalse(answer.IsNullOrEmpty(), $"answer is invalid: {qid}");
                    Assert.IsTrue(origin.Length == answer.Length, $"length is invalid: {qid}");

                    // anagram
                    var originWords = origin.OrderBy(x => x).ToArray();
                    var answerWords = answer.OrderBy(x => x).ToArray();
                    var isEqual = originWords.SequenceEqual(answerWords);
                    Assert.IsTrue(isEqual, $"question is not anagram: {qid}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"unity1week's question master error: {e}");
                throw;
            }
        });
    }

    public static class Utility
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}