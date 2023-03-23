using Tsutaeru.OutGame.Domain.UseCase;
using Tsutaeru.OutGame.Presentation.Presenter;
using Tsutaeru.OutGame.Presentation.View;
using VContainer;
using VContainer.Unity;

namespace Tsutaeru.OutGame.Installer
{
    public sealed class OutGameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<SceneUseCase>(Lifetime.Singleton);

            // Presenter
            builder.RegisterEntryPoint<ScenePresenter>();

            // View
            builder.RegisterInstance<TransitionView>(FindObjectOfType<TransitionView>());
        }
    }
}