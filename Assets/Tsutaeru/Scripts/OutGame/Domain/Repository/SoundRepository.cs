using Tsutaeru.OutGame.Data.DataStore;

namespace Tsutaeru.OutGame.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly BgmTable _bgmTable;
        private readonly SeTable _seTable;

        public SoundRepository(BgmTable bgmTable, SeTable seTable)
        {
            _bgmTable = bgmTable;
            _seTable = seTable;
        }

        public BgmData Find(BgmType type)
        {
            return _bgmTable.data.Find(x => x.type == type);
        }

        public SeData Find(SeType type)
        {
            return _seTable.data.Find(x => x.type == type);
        }
    }
}