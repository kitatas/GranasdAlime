using System;
using Tsutaeru.OutGame.Domain.Repository;
using UniRx;
using UnityEngine;

namespace Tsutaeru.OutGame.Domain.UseCase
{
    public sealed class SoundUseCase
    {
        private readonly ReactiveProperty<Data.DataStore.BgmData> _playBgm;
        private readonly Subject<Unit> _stopBgm;
        private readonly Subject<Data.DataStore.SeData> _playSe;

        private readonly SoundRepository _soundRepository;

        public SoundUseCase(SoundRepository soundRepository)
        {
            _playBgm = new ReactiveProperty<Data.DataStore.BgmData>();
            _stopBgm = new Subject<Unit>();
            _playSe = new Subject<Data.DataStore.SeData>();
            _soundRepository = soundRepository;
        }

        public IObservable<AudioClip> playBgm => _playBgm.SkipLatestValueOnSubscribe().Select(x => x.clip);
        public IObservable<Unit> stopBgm => _stopBgm;
        public IObservable<AudioClip> playSe => _playSe.Select(x => x.clip);

        public void PlayBgm(BgmType type)
        {
            var data = _soundRepository.Find(type);
            _playBgm.Value = data;
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
    }
}