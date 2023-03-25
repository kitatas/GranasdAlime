using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.OutGame.Domain.UseCase;
using Tsutaeru.OutGame.Presentation.View;
using UniRx;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Tsutaeru.OutGame.Presentation.Presenter
{
    public sealed class ScenePresenter : IInitializable, IDisposable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly TransitionView _transitionView;
        private readonly CancellationTokenSource _tokenSource;

        public ScenePresenter(SceneUseCase sceneUseCase, TransitionView transitionView)
        {
            _sceneUseCase = sceneUseCase;
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
                    FadeLoadAsync(x, _tokenSource.Token).Forget();
                })
                .AddTo(_transitionView);
        }

        private async UniTaskVoid FadeLoadAsync(SceneName sceneName, CancellationToken token)
        {
            await _transitionView.FadeInAsync(SceneConfig.FADE_IN_TIME, token);
            await SceneManager.LoadSceneAsync(sceneName.ToString()).WithCancellation(token);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
            await _transitionView.FadeOutAsync(SceneConfig.FADE_OUT_TIME, token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}