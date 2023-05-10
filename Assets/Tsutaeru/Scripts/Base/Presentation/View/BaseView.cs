using UnityEngine;

namespace Tsutaeru.Base.Presentation.View
{
    public abstract class BaseView<T> : MonoBehaviour where T : new()
    {
        public abstract void Render(T value);
    }
}