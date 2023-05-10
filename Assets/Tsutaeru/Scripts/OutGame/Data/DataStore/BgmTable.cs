using Tsutaeru.Base.Data.DataStore;
using UnityEngine;

namespace Tsutaeru.OutGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BgmTable), menuName = "DataTable/" + nameof(BgmTable))]
    public sealed class BgmTable : BaseTable<BgmData>
    {
    }
}