using System.Collections.Generic;
using UnityEngine;

namespace Tsutaeru.Common.Data.Container
{
    public abstract class BaseContainer<T> where T : MonoBehaviour
    {
        protected readonly List<T> _list;

        public BaseContainer()
        {
            _list = new List<T>();
        }

        public void Add(T t)
        {
            _list.Add(t);
        }

        public void Clear()
        {
            _list.Clear();
        }
    }
}