using Tsutaeru.Base.Data.Entity;

namespace Tsutaeru.InGame.Data.Entity
{
    public sealed class TimeEntity : BaseEntity<float>
    {
        public TimeEntity()
        {
            Set(0.0f);
        }

        public void Add(float addValue)
        {
            Set(value + addValue);
        }
    }
}