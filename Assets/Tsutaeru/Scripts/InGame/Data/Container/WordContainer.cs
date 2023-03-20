using System;
using Tsutaeru.Common.Data.Container;
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
                _ => throw new ArgumentOutOfRangeException(nameof(moveStatus), moveStatus, null)
            };

            (_list[index], _list[index + addIndex]) = (_list[index + addIndex], _list[index]);
            (_list[index].wordIndex, _list[index + addIndex].wordIndex) =
                (_list[index + addIndex].wordIndex, _list[index].wordIndex);
            (_list[index].wordStatus, _list[index + addIndex].wordStatus) =
                (_list[index + addIndex].wordStatus, _list[index].wordStatus);

            _list[index].transform.AddLocalPositionX(WordConfig.INTERVAL * -addIndex);
        }
    }
}