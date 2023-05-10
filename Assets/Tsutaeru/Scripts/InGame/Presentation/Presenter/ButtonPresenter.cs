using System;
using System.Threading;
using Tsutaeru.Base.Presentation.View;
using Tsutaeru.InGame.Presentation.View;
using Tsutaeru.OutGame.Domain.UseCase;
using UniRx;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Tsutaeru.InGame.Presentation.Presenter
{
    public sealed class ButtonPresenter : IInitializable, IDisposable
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly VolumeView _volumeView;
        private readonly CancellationTokenSource _tokenSource;

        public ButtonPresenter(SoundUseCase soundUseCase, VolumeView volumeView)
        {
            _soundUseCase = soundUseCase;
            _volumeView = volumeView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            foreach (var buttonView in Object.FindObjectsOfType<BaseButtonView>())
            {
                buttonView.InitAsync(_soundUseCase.PlaySe, _tokenSource.Token).Forget();
            }

            _volumeView.Init(_soundUseCase.bgmVolumeValue, _soundUseCase.seVolumeValue);

            _volumeView.updateBgmVolume
                .Subscribe(_soundUseCase.SetBgmVolume)
                .AddTo(_volumeView);

            _volumeView.updateSeVolume
                .Subscribe(_soundUseCase.SetSeVolume)
                .AddTo(_volumeView);

            _volumeView.releaseVolume
                .Subscribe(_soundUseCase.PlaySe)
                .AddTo(_volumeView);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}