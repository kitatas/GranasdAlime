using System;
using System.Threading;
using Tsutaeru.Base.Presentation.View;
using Tsutaeru.Common.Domain.UseCase;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Tsutaeru.Boot.Presentation.Presenter
{
    public sealed class ButtonPresenter : IInitializable, IDisposable
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly CancellationTokenSource _tokenSource;

        public ButtonPresenter(SoundUseCase soundUseCase)
        {
            _soundUseCase = soundUseCase;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            foreach (var buttonView in Object.FindObjectsOfType<BaseButtonView>())
            {
                buttonView.InitAsync(_soundUseCase.PlaySe, _tokenSource.Token).Forget();
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}