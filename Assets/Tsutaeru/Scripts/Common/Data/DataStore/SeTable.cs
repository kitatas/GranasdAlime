using Tsutaeru.Base.Data.DataStore;
using UnityEngine;

namespace Tsutaeru.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeTable), menuName = "DataTable/" + nameof(SeTable))]
    public sealed class SeTable : BaseTable<SeData>
    {
    }
}