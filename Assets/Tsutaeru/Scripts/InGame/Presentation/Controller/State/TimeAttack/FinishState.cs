using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common.Domain.UseCase;
using Tsutaeru.InGame.Domain.UseCase;
using Tsutaeru.InGame.Presentation.View;

namespace Tsutaeru.InGame.Presentation.Controller.TimeAttack
{
    public sealed class FinishState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly UserRecordView _userRecordView;

        public FinishState(LoadingUseCase loadingUseCase, UserRecordUseCase userRecordUseCase,
            UserRecordView userRecordView)
        {
            _loadingUseCase = loadingUseCase;
            _userRecordUseCase = userRecordUseCase;
            _userRecordView = userRecordView;
        }

        public override GameState state => GameState.TaFinish;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // ロード表示
            _loadingUseCase.Set(true);

            // ユーザーの記録更新 + ランキング送信
            await _userRecordUseCase.SendTimeAttackScoreAsync(token);
            var score = _userRecordUseCase.GetUserScore();
            _userRecordView.SetScore(score.current, score.high);

            // ランキング反映待ち
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            return GameState.TaResult;
        }
    }
}