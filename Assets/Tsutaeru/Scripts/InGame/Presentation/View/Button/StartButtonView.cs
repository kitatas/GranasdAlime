using System;
using Tsutaeru.Base.Presentation.View;
using Tsutaeru.Common;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class StartButtonView : BaseButtonView
    {
        [SerializeField] private GameMode mode = default;

        public void Init(Action<GameMode> setUpMode)
        {
            push.Subscribe(_ => setUpMode?.Invoke(mode))
                .AddTo(this);
        }
    }
}