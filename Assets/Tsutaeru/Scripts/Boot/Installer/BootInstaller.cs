using Tsutaeru.Boot.Domain.UseCase;
using Tsutaeru.Boot.Presentation.Controller;
using VContainer;
using VContainer.Unity;

namespace Tsutaeru.Boot.Installer
{
    public sealed class BootInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<LoginUseCase>(Lifetime.Scoped);

            // Controller
            builder.RegisterEntryPoint<BootController>(Lifetime.Scoped);
        }
    }
}