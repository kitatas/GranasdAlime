using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly RankingUseCase _rankingUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly SoundUseCase _soundUseCase;
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly HintView _hintView;
        private readonly ReloadButtonView _reloadButtonView;
        private readonly RankingView _rankingView;
        private readonly UserRecordView _userRecordView;

        public ResultState(LoadingUseCase loadingUseCase, RankingUseCase rankingUseCase, SceneUseCase sceneUseCase,
            SoundUseCase soundUseCase, UserRecordUseCase userRecordUseCase, HintView hintView,
            ReloadButtonView reloadButtonView, RankingView rankingView, UserRecordView userRecordView)
        {
            _loadingUseCase = loadingUseCase;
            _rankingUseCase = rankingUseCase;
            _sceneUseCase = sceneUseCase;
            _soundUseCase = soundUseCase;
            _userRecordUseCase = userRecordUseCase;
            _hintView = hintView;
            _reloadButtonView = reloadButtonView;
            _rankingView = rankingView;
            _userRecordView = userRecordView;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _hintView.TweenHeightAsync(-100.0f, 0.0f, token).Forget();
            _rankingView.HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _soundUseCase.PlaySe(SeType.Hint);
            await _hintView.ResetAsync(UiConfig.ANIMATION_TIME, token);

            // ロード表示
            _loadingUseCase.Set(true);

            // ユーザーの記録更新 + ランキング送信
            await _userRecordUseCase.SendTimeAttackScoreAsync(token);
            var score = _userRecordUseCase.GetUserScore();
            _userRecordView.SetScore(score.current, score.high);

            // ランキング反映待ち
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            var records = await _rankingUseCase.GetTimeAttackRankingAsync(token);
            _rankingView.SetUp(records);

            // ロード非表示
            _loadingUseCase.Set(false);

            _soundUseCase.PlaySe(SeType.Hint);
            await _hintView.RenderAsync("タイムアタック ランキング", UiConfig.ANIMATION_TIME, token);
            await _hintView.TweenHeightAsync(-50.0f, UiConfig.ANIMATION_TIME, token);

            await _rankingView.ShowAsync(UiConfig.POPUP_TIME, token);

            await _reloadButtonView.ShowAsync(UiConfig.ANIMATION_TIME, token);
            await _reloadButtonView.push.ToUniTask(true, token);

            _sceneUseCase.Load(SceneName.Main, LoadType.Fade);

            return GameState.None;
        }
    }
}