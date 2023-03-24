using UnityEngine;

namespace Tsutaeru.OutGame.Presentation.View
{
    public sealed class SoundView : MonoBehaviour
    {
        [SerializeField] private AudioSource seSource = default;

        public void PlaySe(AudioClip clip)
        {
            seSource.PlayOneShot(clip);
        }
    }
}