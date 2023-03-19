using System.Collections.Generic;
using UnityEngine;

namespace Tsutaeru.Common.Data.DataStore
{
    public abstract class BaseTable<T> : ScriptableObject where T : ScriptableObject
    {
        [SerializeField] private List<T> dataList = default;

        public List<T> data => dataList;
    }
}