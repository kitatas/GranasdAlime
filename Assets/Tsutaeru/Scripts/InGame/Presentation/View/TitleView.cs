using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class TitleView : MonoBehaviour
    {
        [SerializeField] private ConfigView configView = default;
        [SerializeField] private InformationView informationView = default;
        [SerializeField] private TopView topView = default;

        [SerializeField] private StartButtonView timeAttackButton = default;
        [SerializeField] private StartButtonView scoreAttackButton = default;

        private readonly Subject<GameMode> _gameMode = new Subject<GameMode>();
        public IObservable<GameMode> SelectGameMode() => _gameMode;

        public async UniTaskVoid InitAsync(float animationTime, CancellationToken token)
        {
            configView.InitAsync(animationTime, token).Forget();
            informationView.InitAsync(animationTime, token).Forget();
            topView.InitAsync(animationTime, token).Forget();

            configView.hideConfig += () => topView.ShowAsync(animationTime, token).Forget();
            informationView.hideInformation += () => topView.ShowAsync(animationTime, token).Forget();
            topView.showConfig += () => configView.ShowAsync(animationTime, token).Forget();
            topView.showInformation += () => informationView.ShowAsync(animationTime, token).Forget();

            timeAttackButton.Init(x => _gameMode?.OnNext(x));
            scoreAttackButton.Init(x => _gameMode?.OnNext(x));

            await UniTask.Yield(token);
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await topView.HideAsync(animationTime, token);
        }
    }
}