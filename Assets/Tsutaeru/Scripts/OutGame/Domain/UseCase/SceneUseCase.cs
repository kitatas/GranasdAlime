using System;
using UniRx;

namespace Tsutaeru.OutGame.Domain.UseCase
{
    public sealed class SceneUseCase
    {
        private readonly Subject<SceneName> _load;

        public SceneUseCase()
        {
            _load = new Subject<SceneName>();
        }

        public IObservable<SceneName> load => _load.Where(x => x != SceneName.None);

        public void Load(SceneName sceneName)
        {
            _load?.OnNext(sceneName);
        }
    }
}