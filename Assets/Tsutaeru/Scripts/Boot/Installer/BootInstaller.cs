using Tsutaeru.Boot.Domain.UseCase;
using Tsutaeru.Boot.Presentation.Controller;
using Tsutaeru.Boot.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tsutaeru.Boot.Installer
{
    public sealed class BootInstaller : LifetimeScope
    {
        [SerializeField] private RegisterView registerView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<LoginUseCase>(Lifetime.Scoped);

            // Controller
            builder.RegisterEntryPoint<BootController>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<RegisterView>(registerView);
        }
    }
}