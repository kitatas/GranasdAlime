using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.OutGame.Domain.UseCase;
using Tsutaeru.OutGame.Presentation.View;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Tsutaeru.OutGame.Presentation.Presenter
{
    public sealed class ScenePresenter : IInitializable, ITickable, IDisposable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly SoundUseCase _soundUseCase;
        private readonly TransitionView _transitionView;
        private readonly CancellationTokenSource _tokenSource;
        private bool _isFade;

        public ScenePresenter(SceneUseCase sceneUseCase, SoundUseCase soundUseCase, TransitionView transitionView)
        {
            _sceneUseCase = sceneUseCase;
            _soundUseCase = soundUseCase;
            _transitionView = transitionView;
            _tokenSource = new CancellationTokenSource();
            _isFade = false;
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
            _isFade = true;
            _soundUseCase.PlaySe(SeType.Transition);
            _soundUseCase.StopBgm();
            await _transitionView.FadeInAsync(SceneConfig.FADE_IN_TIME, token);
            await SceneManager.LoadSceneAsync(sceneName.ToString()).WithCancellation(token);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            _soundUseCase.PlayBgm(BgmType.Title);
            await _transitionView.FadeOutAsync(SceneConfig.FADE_OUT_TIME, token);
            _isFade = false;
        }

        public void Tick()
        {
            if (_isFade)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _sceneUseCase.Reload();
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}