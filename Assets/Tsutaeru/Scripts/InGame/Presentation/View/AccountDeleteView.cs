using System;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class AccountDeleteView : MonoBehaviour
    {
        [SerializeField] private DecisionButtonView decision = default;

        public IObservable<Unit> DeleteDecision()
        {
            return decision.push;
        }
    }
}