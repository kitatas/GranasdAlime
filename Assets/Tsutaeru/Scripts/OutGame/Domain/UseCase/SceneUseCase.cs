using System;
using Tsutaeru.OutGame.Data.Entity;
using UniRx;

namespace Tsutaeru.OutGame.Domain.UseCase
{
    public sealed class SceneUseCase
    {
        private readonly Subject<LoadEntity> _load;

        public SceneUseCase()
        {
            _load = new Subject<LoadEntity>();
        }

        public IObservable<LoadEntity> load => _load.Where(x => x.sceneName != SceneName.None);

        public void Load(SceneName sceneName, LoadType loadType)
        {
            var loadEntity = new LoadEntity(sceneName, loadType);
            _load?.OnNext(loadEntity);
        }

        public void Reload()
        {
            Load(SceneName.Main, LoadType.Fade);
        }
    }
}