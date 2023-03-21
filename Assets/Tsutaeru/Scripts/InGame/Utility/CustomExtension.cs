using System;

namespace Tsutaeru.InGame
{
    public static class CustomExtension
    {
        public static string ToWord(this HintType type)
        {
            return type switch
            {
                HintType.Drink => "ノミモノ",
                HintType.Jewel => "ホウセキ",
                HintType.Food  => "タベモノ",
                HintType.Fluid => "リュウタイ",
                HintType.Sport => "スポーツ",
                HintType.Item => "アイテム",
                HintType.Country => "クニ",
                HintType.Action => "コウドウ",
                HintType.Service => "サービス",
                HintType.Metal => "キンゾク",
                HintType.Creature => "セイブツ",
                HintType.Grain => "リュウシ",
                HintType.Data => "データ",
                HintType.Human => "ヒト",
                HintType.Vehicle => "ノリモノ",
                HintType.Rule => "ゲームルール",
                HintType.Title => "ゲームタイトル",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}