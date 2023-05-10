using Tsutaeru.Base.Presentation.Presenter;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;
using UnityEngine;
using VContainer.Unity;

namespace Tsutaeru.InGame.Presentation.Presenter
{
    public sealed class TimePresenter : BasePresenter<float>, ITickable
    {
        private readonly TimeUseCase _timeUseCase;
        private readonly TimeView _timeView;

        public TimePresenter(TimeUseCase timeUseCase, TimeView timeView) : base(timeUseCase, timeView)
        {
            _timeUseCase = timeUseCase;
            _timeView = timeView;
        }

        public void Tick()
        {
            _timeUseCase.Tick(Time.deltaTime);
        }
    }
}