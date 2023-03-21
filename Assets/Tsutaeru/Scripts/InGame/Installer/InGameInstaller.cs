using Tsutaeru.InGame.Data.Container;
using Tsutaeru.InGame.Data.DataStore;
using Tsutaeru.InGame.Data.Entity;
using Tsutaeru.InGame.Domain.Factory;
using Tsutaeru.InGame.Domain.Repository;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.Controller;
using Tsutaeru.InGame.Presentation.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tsutaeru.InGame.Installer
{
    public sealed class InGameInstaller : LifetimeScope
    {
        [SerializeField] private PrefabTable prefabTable = default;
        [SerializeField] private QuestionTable questionTable = default;

        [SerializeField] private RectTransform wordParent = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Container
            builder.Register<WordContainer>(Lifetime.Scoped);

            // DataStore
            builder.RegisterInstance<PrefabTable>(prefabTable);
            builder.RegisterInstance<QuestionTable>(questionTable);

            // Entity
            builder.Register<StateEntity>(Lifetime.Scoped);

            // Factory
            builder.Register<WordFactory>(Lifetime.Scoped).WithParameter(wordParent);

            // Repository
            builder.Register<PrefabRepository>(Lifetime.Scoped);
            builder.Register<QuestionRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<QuestionUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);
            builder.Register<WordUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<StatePresenter>();
        }
    }
}