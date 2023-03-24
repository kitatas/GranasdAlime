using System;
using Tsutaeru.OutGame.Data.DataStore;

namespace Tsutaeru.OutGame.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly SeTable _seTable;

        public SoundRepository(SeTable seTable)
        {
            _seTable = seTable;
        }

        public SeData Find(SeType type)
        {
            var data = _seTable.data.Find(x => x.type == type);
            if (data == null)
            {
                throw new Exception($"se data is null. (type: {type})");
            }

            if (data.clip == null)
            {
                throw new Exception($"se clip is null. (type: {type})");
            }

            return data;
        }
    }
}