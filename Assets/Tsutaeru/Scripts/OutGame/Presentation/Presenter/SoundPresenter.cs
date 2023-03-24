using Tsutaeru.OutGame.Domain.UseCase;
using Tsutaeru.OutGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Tsutaeru.OutGame.Presentation.Presenter
{
    public sealed class SoundPresenter : IInitializable
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
            _soundUseCase.playSe
                .Subscribe(_soundView.PlaySe)
                .AddTo(_soundView);
        }
    }
}