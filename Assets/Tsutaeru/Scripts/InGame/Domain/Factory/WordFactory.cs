using UniEx;
using UnityEngine;

namespace Tsutaeru.InGame.Domain.Factory
{
    public sealed class WordFactory
    {
        private readonly RectTransform _wordParent;
        
        public WordFactory(RectTransform wordParent)
        {
            _wordParent = wordParent;
        }

        public Presentation.View.WordView Generate(Presentation.View.WordView view, float x)
        {
            var instance = Object.Instantiate(view, _wordParent);
            instance.transform.SetLocalPositionX(x);
            return instance;
        }
    }
}