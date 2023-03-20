using Tsutaeru.InGame.Domain.Factory;
using Tsutaeru.InGame.Domain.Repository;
using UniEx;
using UnityEngine;

namespace Tsutaeru.InGame.Domain.UseCase
{
    public sealed class WordUseCase
    {
        private readonly WordFactory _wordFactory;
        private readonly PrefabRepository _prefabRepository;

        public WordUseCase(WordFactory wordFactory, PrefabRepository prefabRepository)
        {
            _wordFactory = wordFactory;
            _prefabRepository = prefabRepository;
        }

        public void Build(Data.DataStore.QuestionData data)
        {
            var length = data.origin.Length;
            var x = length.IsEven()
                ? -1.0f * (WordConfig.INTERVAL * length / 2.0f + WordConfig.INTERVAL / 2.0f)
                : -1.0f * (WordConfig.INTERVAL * Mathf.Floor(length / 2.0f));

            for (int i = 0; i < length; i++)
            {
                var view = _prefabRepository.GetWordView();
                var instance = _wordFactory.Generate(view, x);
                instance.Init(data.origin[i]);

                x += WordConfig.INTERVAL;
            }
        }
    }
}