using System;
using Tsutaeru.OutGame.Data.Entity;
using UniRx;

namespace Tsutaeru.OutGame.Domain.UseCase
{
    public sealed class SceneUseCase
    {
        private readonly Subject<LoadEntity> _load;
        private readonly InGame.Data.Entity.StateEntity _stateEntity;

        public SceneUseCase(InGame.Data.Entity.StateEntity stateEntity)
        {
            _load = new Subject<LoadEntity>();
            _stateEntity = stateEntity;
        }

        public IObservable<LoadEntity> load => _load.Where(x => x.sceneName != SceneName.None);

        public void Load(SceneName sceneName, LoadType loadType)
        {
            var loadEntity = new LoadEntity(sceneName, loadType);
            _load?.OnNext(loadEntity);
        }

        public void Reload()
        {
            if (_stateEntity.IsState(InGame.GameState.Result))
            {
                return;
            }

            Load(SceneName.Main, LoadType.Fade);
        }
    }
}