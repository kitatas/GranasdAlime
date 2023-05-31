using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class StateController
    {
        private readonly List<BaseState> _states;

        public StateController(TitleState titleState, SetUpState setUpState, InputState inputState,
            JudgeState judgeState, ClearState clearState, FinishState finishState, ResultState resultState)
        {
            _states = new List<BaseState>
            {
                titleState,
                setUpState,
                inputState,
                judgeState,
                clearState,
                finishState,
                resultState,
            };
        }

        public async UniTaskVoid InitAsync(CancellationToken token)
        {
            foreach (var state in _states)
            {
                state.InitAsync(token).Forget();
            }

            await UniTask.Yield(token);
        }

        public async UniTask<GameState> TickAsync(GameState state, CancellationToken token)
        {
            var currentState = _states.Find(x => x.state == state);
            if (currentState == null)
            {
                throw new Exception(ExceptionConfig.NOT_FOUND_STATE);
            }

            return await currentState.TickAsync(token);
        }
    }
}