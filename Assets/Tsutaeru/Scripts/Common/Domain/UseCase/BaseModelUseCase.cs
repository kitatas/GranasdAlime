using UniRx;

namespace Tsutaeru.Common.Domain.UseCase
{
    public abstract class BaseModelUseCase<T> where T : new()
    {
        private readonly ReactiveProperty<T> _property;

        public BaseModelUseCase()
        {
            _property = new ReactiveProperty<T>();
        }

        public IReadOnlyReactiveProperty<T> property => _property;

        public virtual void Set(T value)
        {
            _property.Value = value;
        }
    }
}