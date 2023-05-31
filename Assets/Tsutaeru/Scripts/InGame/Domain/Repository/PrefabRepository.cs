using System;
using Tsutaeru.Common;
using Tsutaeru.InGame.Data.DataStore;

namespace Tsutaeru.InGame.Domain.Repository
{
    public sealed class PrefabRepository
    {
        private readonly PrefabTable _prefabTable;

        public PrefabRepository(PrefabTable prefabTable)
        {
            _prefabTable = prefabTable;
        }

        public Presentation.View.WordView GetWordView()
        {
            var data = _prefabTable.word;
            if (data == null)
            {
                throw new Exception(ExceptionConfig.NOT_FOUND_PREFAB);
            }

            return data;
        }
    }
}