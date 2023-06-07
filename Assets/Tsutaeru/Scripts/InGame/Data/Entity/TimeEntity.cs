using Tsutaeru.Base.Data.Entity;

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
            Add(-subValue);
        }
    }
}