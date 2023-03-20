using Tsutaeru.Common.Data.DataStore;
using UnityEngine;

namespace Tsutaeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(QuestionTable), menuName = "DataTable/" + nameof(QuestionTable))]
    public sealed class QuestionTable : BaseTable<QuestionData>
    {
    }
}