using TMPro;
using Tsutaeru.Common.Presentation.View;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class TimeView : BaseView<float>
    {
        [SerializeField] private TextMeshProUGUI time = default;

        public override void Render(float value)
        {
            time.text = $"{value:0.00}";
        }
    }
}