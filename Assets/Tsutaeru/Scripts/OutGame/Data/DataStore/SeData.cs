using UnityEngine;

namespace Tsutaeru.OutGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeData), menuName = "DataTable/" + nameof(SeData))]
    public sealed class SeData : ScriptableObject
    {
        [SerializeField] private SeType seType = default;
        [SerializeField] private AudioClip audioClip = default;

        public SeType type => seType;
        public AudioClip clip => audioClip;
    }
}