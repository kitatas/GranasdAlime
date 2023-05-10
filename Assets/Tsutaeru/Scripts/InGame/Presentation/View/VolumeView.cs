using System;
using Tsutaeru.Common;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class VolumeView : MonoBehaviour
    {
        [SerializeField] private Slider bgm = default;
        [SerializeField] private Slider se = default;

        public void Init(float bgmVolume, float seVolume)
        {
            bgm.value = bgmVolume;
            se.value = seVolume;
        }

        public IObservable<float> updateBgmVolume => bgm.OnValueChangedAsObservable();
        public IObservable<float> updateSeVolume => se.OnValueChangedAsObservable();

        public IObservable<SeType> releaseVolume => releaseBgmVolume
            .Merge(releaseSeVolume)
            .Select(_ => SeType.Decision);

        private IObservable<PointerEventData> releaseBgmVolume => bgm.OnPointerClickAsObservable();
        private IObservable<PointerEventData> releaseSeVolume => se.OnPointerClickAsObservable();
    }
}