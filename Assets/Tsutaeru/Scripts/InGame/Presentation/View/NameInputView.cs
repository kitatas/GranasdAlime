using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class NameInputView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField = default;
        [SerializeField] private DecisionButtonView decision = default;

        public void Init(string userName)
        {
            inputField.text = $"{userName}";
        }

        private string inputName => inputField.text;

        public IObservable<string> UpdateName()
        {
            return decision.push.Select(_ => inputName);
        }
    }
}