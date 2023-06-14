using Tsutaeru.Base.Data.Entity;
using Tsutaeru.Common;

namespace Tsutaeru.InGame.Data.Entity
{
    public sealed class ProgressEntity : BaseEntity<int>
    {
        public ProgressEntity()
        {
            Set(0);
        }

        public void Increase()
        {
            Set(value + 1);
        }

        public bool IsCount(int count)
        {
            return value == count;
        }

        public Difficulty GetDifficulty()
        {
            if (value < ProgressConfig.EASY) return Difficulty.Easy;
            if (value < ProgressConfig.NORMAL) return Difficulty.Normal;
            if (value < ProgressConfig.HARD) return Difficulty.Hard;
            if (value < ProgressConfig.SPECIAL) return Difficulty.Special;

            throw new CrashException(ExceptionConfig.NOT_FOUND_PROGRESS);
        }
    }
}