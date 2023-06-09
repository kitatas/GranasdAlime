using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tsutaeru.Common;
using Tsutaeru.InGame.Data.Container;
using Tsutaeru.InGame.Data.Entity;
using Tsutaeru.InGame.Domain.Factory;
using Tsutaeru.InGame.Domain.Repository;
using UniEx;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class WordUseCase
    {
        private QuestionEntity _questionEntity;

        private readonly Subject<Unit> _execShift;
        private readonly WordContainer _wordContainer;
        private readonly StateEntity _stateEntity;
        private readonly WordFactory _wordFactory;
        private readonly PrefabRepository _prefabRepository;

        public WordUseCase(WordContainer wordContainer, StateEntity stateEntity,
            WordFactory wordFactory, PrefabRepository prefabRepository)
        {
            _execShift = new Subject<Unit>();
            _wordContainer = wordContainer;
            _stateEntity = stateEntity;
            _wordFactory = wordFactory;
            _prefabRepository = prefabRepository;
        }

        public async UniTask BuildAsync(QuestionEntity question, Action<SeType> playSe, CancellationToken token)
        {
            _wordContainer.Refresh();
            _questionEntity = question;

            var length = _questionEntity.questionLength;
            var pointX = length.IsEven()
                ? -1.0f * (WordConfig.INTERVAL * (length / 2.0f - 1.0f) + WordConfig.INTERVAL / 2.0f)
                : -1.0f * (WordConfig.INTERVAL * Mathf.Floor(length / 2.0f));

            for (int i = 0; i < length; i++)
            {
                var status =
                    (i == 0) ? WordStatus.First :
                    (i == length - 1) ? WordStatus.Last : WordStatus.Middle;

                var view = _prefabRepository.GetWordView();
                var instance = _wordFactory.Generate(view, pointX);
                instance.Init(_questionEntity.GetQuestionChar(i), i, status,
                    () => _stateEntity.IsState(GameState.TaInput),
                    x =>
                    {
                        playSe?.Invoke(SeType.Pop);
                        _wordContainer.Shift(x.index, x.move);
                    },
                    () => _execShift?.OnNext(Unit.Default));

                _wordContainer.Add(instance);

                playSe?.Invoke(SeType.Pop);

                pointX += WordConfig.INTERVAL;

                await UniTask.Delay(TimeSpan.FromSeconds(WordConfig.GENERATE_SPEED), cancellationToken: token);
            }
        }

        public async UniTask HideAllWordAsync(CancellationToken token)
        {
            _wordContainer.HideAll();
            await UniTask.Delay(TimeSpan.FromSeconds(WordConfig.GENERATE_SPEED), cancellationToken: token);
        }

        public async UniTask HideAllWordBackgroundAsync(CancellationToken token)
        {
            _wordContainer.HideBackgroundAll();
            await UniTask.Delay(TimeSpan.FromSeconds(WordConfig.GENERATE_SPEED), cancellationToken: token);
        }

        public IObservable<Unit> ExecShift() => _execShift;

        public bool IsCorrect()
        {
            var userAnswer = _wordContainer.GetUserAnswer();
            return _questionEntity.IsCorrectAnswer(userAnswer);
        }
    }
}