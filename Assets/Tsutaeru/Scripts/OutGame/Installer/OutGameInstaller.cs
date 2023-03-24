using Tsutaeru.OutGame.Data.DataStore;
using Tsutaeru.OutGame.Domain.Repository;
using Tsutaeru.OutGame.Domain.UseCase;
using Tsutaeru.OutGame.Presentation.Presenter;
using Tsutaeru.OutGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tsutaeru.OutGame.Installer
{
    public sealed class OutGameInstaller : LifetimeScope
    {
        [SerializeField] private BgmTable bgmTable = default;
        [SerializeField] private SeTable seTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<BgmTable>(bgmTable);
            builder.RegisterInstance<SeTable>(seTable);

            // Repository
            builder.Register<SoundRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<SceneUseCase>(Lifetime.Singleton);
            builder.Register<SoundUseCase>(Lifetime.Singleton);

            // Presenter
            builder.RegisterEntryPoint<ScenePresenter>();
            builder.RegisterEntryPoint<SoundPresenter>();

            // View
            builder.RegisterInstance<SoundView>(FindObjectOfType<SoundView>());
            builder.RegisterInstance<TransitionView>(FindObjectOfType<TransitionView>());
        }
    }
}