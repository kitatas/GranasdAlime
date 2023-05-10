using System;
using Tsutaeru.Common.Domain.Repository;
using UniRx;
using UnityEngine;

namespace Tsutaeru.Common.Domain.UseCase
{
    public sealed class SoundUseCase
    {
        private readonly Subject<Data.DataStore.BgmData> _playBgm;
        private readonly Subject<Unit> _stopBgm;
        private readonly Subject<Data.DataStore.SeData> _playSe;
        private readonly ReactiveProperty<float> _bgmVolume;
        private readonly ReactiveProperty<float> _seVolume;

        private readonly SaveRepository _saveRepository;
        private readonly SoundRepository _soundRepository;

        public SoundUseCase(SaveRepository saveRepository, SoundRepository soundRepository)
        {
            _saveRepository = saveRepository;
            _soundRepository = soundRepository;

            var data = _saveRepository.Load();
            _playBgm = new Subject<Data.DataStore.BgmData>();
            _stopBgm = new Subject<Unit>();
            _playSe = new Subject<Data.DataStore.SeData>();
            _bgmVolume = new ReactiveProperty<float>(data.bgmVolume);
            _seVolume = new ReactiveProperty<float>(data.seVolume);
        }

        public IObservable<AudioClip> playBgm => _playBgm.Select(x => x.clip);
        public IObservable<Unit> stopBgm => _stopBgm;
        public IObservable<AudioClip> playSe => _playSe.Select(x => x.clip);
        public IObservable<float> bgmVolume => _bgmVolume.Select(x => x / 10.0f);
        public IObservable<float> seVolume => _seVolume.Select(x => x / 10.0f);
        public float bgmVolumeValue => _bgmVolume.Value;
        public float seVolumeValue => _seVolume.Value;

        public void PlayBgm(BgmType type)
        {
            var data = _soundRepository.Find(type);
            _playBgm?.OnNext(data);
        }

        public void StopBgm()
        {
            _stopBgm?.OnNext(Unit.Default);
        }

        public void PlaySe(SeType type)
        {
            var data = _soundRepository.Find(type);
            _playSe?.OnNext(data);
        }

        public void SetBgmVolume(float value)
        {
            _bgmVolume.Value = value;
            _saveRepository.SaveBgm(value);
        }

        public void SetSeVolume(float value)
        {
            _seVolume.Value = value;
            _saveRepository.SaveSe(value);
        }
    }
}