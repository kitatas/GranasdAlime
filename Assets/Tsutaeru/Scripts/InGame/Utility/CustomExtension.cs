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
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}