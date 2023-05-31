using System;
using System.Linq;
using Tsutaeru.Base.Data.Container;
using Tsutaeru.Common;
using UniEx;

namespace Tsutaeru.InGame.Data.Container
{
    public sealed class WordContainer : BaseContainer<Presentation.View.WordView>
    {
        public void Shift(int index, MoveStatus moveStatus)
        {
            // 入れ替え対象は逆方向への移動になる
            var addIndex = moveStatus switch
            {
                MoveStatus.Left  => 1,
                MoveStatus.Right => -1,
                _ => throw new Exception(ExceptionConfig.UNMATCHED_TYPE_MOVE),
            };

            (_list[index], _list[index + addIndex]) = (_list[index + addIndex], _list[index]);
            (_list[index].wordIndex, _list[index + addIndex].wordIndex) =
                (_list[index + addIndex].wordIndex, _list[index].wordIndex);
            (_list[index].wordStatus, _list[index + addIndex].wordStatus) =
                (_list[index + addIndex].wordStatus, _list[index].wordStatus);

            _list[index].TweenShift(WordConfig.INTERVAL * -addIndex);
        }

        public string GetUserAnswer()
        {
            var chars = _list.Select(x => x.wordChar).ToArray();
            return new string(chars);
        }

        public void Refresh()
        {
            _list.Select(x => x.gameObject).ToList().DestroyAll();
            _list.Clear();
        }

        public void HideAll()
        {
            _list.Each(x => x.Hide());
        }

        public void HideBackgroundAll()
        {
            _list.Each(x => x.HideBackground());
        }
    }
}