using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Tsutaeru.Common.Presentation.Presenter
{
    public abstract class BasePresenter<T> : IInitializable where T : new()
    {
        private readonly BaseModelUseCase<T> _modelUseCase;
        private readonly BaseView<T> _view;

        public BasePresenter(BaseModelUseCase<T> modelUseCase, BaseView<T> view)
        {
            _modelUseCase = modelUseCase;
            _view = view;
        }

        public void Initialize()
        {
            _modelUseCase.property
                .Subscribe(_view.Render)
                .AddTo(_view);
        }
    }
}