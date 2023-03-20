using Tsutaeru.InGame.Presentation.View;
using UnityEngine;

namespace Tsutaeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(PrefabTable), menuName = "DataTable/" + nameof(PrefabTable))]
    public sealed class PrefabTable : ScriptableObject
    {
        [SerializeField] private WordView wordView = default;

        public WordView word => wordView;
    }
}