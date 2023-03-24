using System;
using System.Threading;
using Tsutaeru.Common.Presentation.View;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Tsutaeru.InGame.Presentation.Presenter
{
    public sealed class ButtonPresenter : IInitializable, IDisposable
    {
        private readonly CancellationTokenSource _tokenSource;

        public ButtonPresenter()
        {
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            foreach (var buttonView in Object.FindObjectsOfType<BaseButtonView>())
            {
                buttonView.InitAsync(_tokenSource.Token).Forget();
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}