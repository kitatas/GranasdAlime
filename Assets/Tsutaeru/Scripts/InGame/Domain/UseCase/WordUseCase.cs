using Tsutaeru.InGame.Data.Container;
using Tsutaeru.InGame.Data.Entity;
using Tsutaeru.InGame.Domain.Factory;
using Tsutaeru.InGame.Domain.Repository;
using UniEx;
using UnityEngine;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class WordUseCase
    {
        private readonly WordContainer _wordContainer;
        private readonly StateEntity _stateEntity;
        private readonly WordFactory _wordFactory;
        private readonly PrefabRepository _prefabRepository;

        public WordUseCase(WordContainer wordContainer, StateEntity stateEntity,
            WordFactory wordFactory, PrefabRepository prefabRepository)
        {
            _wordContainer = wordContainer;
            _stateEntity = stateEntity;
            _wordFactory = wordFactory;
            _prefabRepository = prefabRepository;
        }

        public void Build(Data.DataStore.QuestionData data)
        {
            _wordContainer.Clear();

            var length = data.origin.Length;
            var pointX = length.IsEven()
                ? -1.0f * (WordConfig.INTERVAL * length / 2.0f + WordConfig.INTERVAL / 2.0f)
                : -1.0f * (WordConfig.INTERVAL * Mathf.Floor(length / 2.0f));

            for (int i = 0; i < length; i++)
            {
                var status =
                    (i == 0) ? WordStatus.First :
                    (i == length - 1) ? WordStatus.Last : WordStatus.Middle;

                var view = _prefabRepository.GetWordView();
                var instance = _wordFactory.Generate(view, pointX);
                instance.Init(data.origin[i], i, status,
                    () => _stateEntity.IsState(GameState.Input),
                    x =>
                    {
                        _wordContainer.Shift(x.index, x.move);
                    });

                _wordContainer.Add(instance);

                pointX += WordConfig.INTERVAL;
            }
        }
    }
}