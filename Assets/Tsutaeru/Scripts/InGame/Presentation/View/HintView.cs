using TMPro;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class HintView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hint = default;

        public void Init()
        {
            Set("");
        }

        public void Render(Data.DataStore.QuestionData data)
        {
            Set($"{data.originHint.ToWord()} <size=-10>カラ<size=+0> {data.answerHint.ToWord()} <size=-10>ヘ");
        }

        private void Set(string message)
        {
            hint.text = message;
        }
    }
}