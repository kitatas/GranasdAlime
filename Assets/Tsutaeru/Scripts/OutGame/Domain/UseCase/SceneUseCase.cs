using System;
using UniRx;

namespace Tsutaeru.OutGame.Domain.UseCase
{
    public sealed class SceneUseCase
    {
        private readonly Subject<SceneName> _load;
        private readonly InGame.Data.Entity.StateEntity _stateEntity;

        public SceneUseCase(InGame.Data.Entity.StateEntity stateEntity)
        {
            _load = new Subject<SceneName>();
            _stateEntity = stateEntity;
        }

        public IObservable<SceneName> load => _load.Where(x => x != SceneName.None);

        public void Load(SceneName sceneName)
        {
            _load?.OnNext(sceneName);
        }

        public void Reload()
        {
            if (_stateEntity.IsState(InGame.GameState.Result))
            {
                return;
            }

            Load(SceneName.Main);
        }
    }
}