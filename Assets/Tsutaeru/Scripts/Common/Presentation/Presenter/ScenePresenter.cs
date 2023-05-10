using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.Common.Presentation.View;
using UniRx;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Tsutaeru.Common.Presentation.Presenter
{
    public sealed class ScenePresenter : IInitializable, IDisposable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly SoundUseCase _soundUseCase;
        private readonly TransitionView _transitionView;
        private readonly CancellationTokenSource _tokenSource;

        public ScenePresenter(SceneUseCase sceneUseCase, SoundUseCase soundUseCase, TransitionView transitionView)
        {
            _sceneUseCase = sceneUseCase;
            _soundUseCase = soundUseCase;
            _transitionView = transitionView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _transitionView.FadeOutAsync(0.0f, _tokenSource.Token).Forget();

            _sceneUseCase.load
                .Subscribe(x =>
                {
                    // シーン遷移
                    switch (x.loadType)
                    {
                        case LoadType.Direct:
                            SceneManager.LoadScene(x.sceneName.ToString());
                            break;
                        case LoadType.Fade:
                            FadeLoadAsync(x.sceneName, _tokenSource.Token).Forget();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(x.loadType), x.loadType, null);
                    }
                })
                .AddTo(_transitionView);
        }

        private async UniTaskVoid FadeLoadAsync(SceneName sceneName, CancellationToken token)
        {
            _soundUseCase.PlaySe(SeType.Transition);
            _soundUseCase.StopBgm();
            await _transitionView.FadeInAsync(SceneConfig.FADE_IN_TIME, token);
            await SceneManager.LoadSceneAsync(sceneName.ToString()).WithCancellation(token);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            _soundUseCase.PlayBgm(BgmType.Title);
            await _transitionView.FadeOutAsync(SceneConfig.FADE_OUT_TIME, token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}