using UnityEngine;

namespace Tsutaeru.OutGame.Presentation.View
{
    public sealed class SoundView : MonoBehaviour
    {
        [SerializeField] private AudioSource bgmSource = default;
        [SerializeField] private AudioSource seSource = default;

        public void PlayBgm(AudioClip clip)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }

        public void StopBgm()
        {
            bgmSource.Stop();
        }

        public void PlaySe(AudioClip clip)
        {
            seSource.PlayOneShot(clip);
        }
    }
}