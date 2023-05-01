namespace Tsutaeru.OutGame.Data.Entity
{
    public sealed class LoadEntity
    {
        public readonly SceneName sceneName;
        public readonly LoadType loadType;

        public LoadEntity(SceneName sceneName, LoadType loadType)
        {
            this.sceneName = sceneName;
            this.loadType = loadType;
        }
    }
}