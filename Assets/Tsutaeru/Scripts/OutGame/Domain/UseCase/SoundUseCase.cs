using System;
using Tsutaeru.OutGame.Domain.Repository;
using UniRx;
using UnityEngine;

namespace Tsutaeru.OutGame.Domain.UseCase
{
    public sealed class SoundUseCase
    {
        private readonly Subject<Data.DataStore.SeData> _playSe;

        private readonly SoundRepository _soundRepository;

        public SoundUseCase(SoundRepository soundRepository)
        {
            _playSe = new Subject<Data.DataStore.SeData>();
            _soundRepository = soundRepository;
        }

        public IObservable<AudioClip> playSe => _playSe.Select(x => x.clip);

        public void PlaySe(SeType type)
        {
            var data = _soundRepository.Find(type);
            _playSe?.OnNext(data);
        }
    }
}