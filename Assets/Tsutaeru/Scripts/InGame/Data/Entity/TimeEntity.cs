using Tsutaeru.Base.Data.Entity;
using UnityEngine;

namespace Tsutaeru.InGame.Data.Entity
{
    public sealed class TimeEntity : BaseEntity<float>
    {
        public void Add(float addValue)
        {
            Set(value + addValue);
        }

        public void Subtract(float subValue)
        {
            // 残り時間が負数にならないようにする
            Set(Mathf.Max(value - subValue, 0.0f));
        }
    }
}