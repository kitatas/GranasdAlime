using System;
using Tsutaeru.Common.Data.Entity;

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
    }
}