using Tsutaeru.InGame.Data.DataStore;
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
        [SerializeField] private QuestionTable questionTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<QuestionTable>(questionTable);

            // Repository
            builder.Register<QuestionRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<QuestionUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<StatePresenter>();
        }
    }
}