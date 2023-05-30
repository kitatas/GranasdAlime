using System.Threading;
using Cysharp.Threading.Tasks;

namespace Tsutaeru.Boot.Presentation.Controller
{
    public sealed class LoadState : BaseState
    {
        public override BootState state => BootState.Load;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);

            return BootState.Login;
        }
    }
}