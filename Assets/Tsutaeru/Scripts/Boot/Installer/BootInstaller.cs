using Tsutaeru.Boot.Domain.UseCase;
using Tsutaeru.Boot.Presentation.Controller;
using Tsutaeru.Boot.Presentation.Presenter;
using Tsutaeru.Boot.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tsutaeru.Boot.Installer
{
    public sealed class BootInstaller : LifetimeScope
    {
        [SerializeField] private RegisterView registerView = default;
        [SerializeField] private UpdateView updateView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<AppVersionUseCase>(Lifetime.Scoped);
            builder.Register<LoginUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);
            builder.Register<CheckState>(Lifetime.Scoped);
            builder.Register<LoadState>(Lifetime.Scoped);
            builder.Register<LoginState>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<StatePresenter>();

            // View
            builder.RegisterInstance<RegisterView>(registerView);
            builder.RegisterInstance<UpdateView>(updateView);
        }
    }
}