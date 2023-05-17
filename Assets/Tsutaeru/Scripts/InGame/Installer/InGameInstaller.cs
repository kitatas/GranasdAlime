using Tsutaeru.InGame.Data.Container;
using Tsutaeru.InGame.Data.DataStore;
using Tsutaeru.InGame.Data.Entity;
using Tsutaeru.InGame.Domain.Factory;
using Tsutaeru.InGame.Domain.Repository;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.Controller;
using Tsutaeru.InGame.Presentation.Presenter;
using Tsutaeru.InGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tsutaeru.InGame.Installer
{
    public sealed class InGameInstaller : LifetimeScope
    {
        [SerializeField] private PrefabTable prefabTable = default;

        [SerializeField] private ReloadButtonView reloadButtonView = default;
        [SerializeField] private RetryButtonView retryButtonView = default;
        [SerializeField] private StartButtonView startButtonView = default;
        [SerializeField] private TimeView timeView = default;
        [SerializeField] private TitleView titleView = default;
        [SerializeField] private ProgressView progressView = default;
        [SerializeField] private HintView hintView = default;
        [SerializeField] private VolumeView volumeView = default;
        [SerializeField] private AccountDeleteView accountDeleteView = default;
        [SerializeField] private NameInputView nameInputView = default;
        [SerializeField] private RankingView rankingView = default;

        [SerializeField] private RectTransform wordParent = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Container
            builder.Register<WordContainer>(Lifetime.Scoped);

            // DataStore
            builder.RegisterInstance<PrefabTable>(prefabTable);

            // Entity
            builder.Register<ProgressEntity>(Lifetime.Scoped);
            builder.Register<StateEntity>(Lifetime.Scoped);
            builder.Register<TimeEntity>(Lifetime.Scoped);

            // Factory
            builder.Register<WordFactory>(Lifetime.Scoped).WithParameter(wordParent);

            // Repository
            builder.Register<PrefabRepository>(Lifetime.Scoped);
            builder.Register<QuestionRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<ClearUseCase>(Lifetime.Scoped);
            builder.Register<QuestionUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);
            builder.Register<TimeUseCase>(Lifetime.Scoped);
            builder.Register<RankingUseCase>(Lifetime.Scoped);
            builder.Register<UserDataUseCase>(Lifetime.Scoped);
            builder.Register<UserRecordUseCase>(Lifetime.Scoped);
            builder.Register<WordUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);
            builder.Register<ClearState>(Lifetime.Scoped);
            builder.Register<InputState>(Lifetime.Scoped);
            builder.Register<JudgeState>(Lifetime.Scoped);
            builder.Register<ResultState>(Lifetime.Scoped);
            builder.Register<SetUpState>(Lifetime.Scoped);
            builder.Register<TitleState>(Lifetime.Scoped);
            builder.RegisterEntryPoint<UserDataController>();

            // Presenter
            builder.RegisterEntryPoint<ButtonPresenter>();
            builder.RegisterEntryPoint<StatePresenter>();
            builder.RegisterEntryPoint<TimePresenter>();

            // View
            builder.RegisterInstance<ReloadButtonView>(reloadButtonView);
            builder.RegisterInstance<RetryButtonView>(retryButtonView);
            builder.RegisterInstance<StartButtonView>(startButtonView);
            builder.RegisterInstance<TimeView>(timeView);
            builder.RegisterInstance<TitleView>(titleView);
            builder.RegisterInstance<ProgressView>(progressView);
            builder.RegisterInstance<HintView>(hintView);
            builder.RegisterInstance<VolumeView>(volumeView);
            builder.RegisterInstance<AccountDeleteView>(accountDeleteView);
            builder.RegisterInstance<NameInputView>(nameInputView);
            builder.RegisterInstance<RankingView>(rankingView);
        }
    }
}