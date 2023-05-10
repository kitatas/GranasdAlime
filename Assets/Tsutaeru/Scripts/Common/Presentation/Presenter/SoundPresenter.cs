using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Tsutaeru.Common.Presentation.Presenter
{
    public sealed class SoundPresenter : IInitializable, IStartable
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly SoundView _soundView;

        public SoundPresenter(SoundUseCase soundUseCase, SoundView soundView)
        {
            _soundUseCase = soundUseCase;
            _soundView = soundView;
        }

        public void Initialize()
        {
            _soundUseCase.playBgm
                .Subscribe(_soundView.PlayBgm)
                .AddTo(_soundView);

            _soundUseCase.stopBgm
                .Subscribe(_ => _soundView.StopBgm())
                .AddTo(_soundView);

            _soundUseCase.playSe
                .Subscribe(_soundView.PlaySe)
                .AddTo(_soundView);

            _soundUseCase.bgmVolume
                .Subscribe(_soundView.SetBgmVolume)
                .AddTo(_soundView);

            _soundUseCase.seVolume
                .Subscribe(_soundView.SetSeVolume)
                .AddTo(_soundView);
        }

        public void Start()
        {
            _soundUseCase.PlayBgm(BgmType.Title);
        }
    }
}