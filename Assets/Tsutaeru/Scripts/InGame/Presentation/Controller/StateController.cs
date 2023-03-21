using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Tsutaeru.InGame.Presentation.Controller
{
    public sealed class StateController
    {
        private readonly List<BaseState> _states;

        public StateController(SetUpState setUpState, InputState inputState, JudgeState judgeState)
        {
            _states = new List<BaseState>
            {
                setUpState,
                inputState,
                judgeState,
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
                throw new Exception($"Can't find state. (state: {state})");
            }

            return await currentState.TickAsync(token);
        }
    }
}