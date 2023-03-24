using Tsutaeru.Common.Data.DataStore;
using UnityEngine;

namespace Tsutaeru.OutGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeTable), menuName = "DataTable/" + nameof(SeTable))]
    public sealed class SeTable : BaseTable<SeData>
    {
    }
}