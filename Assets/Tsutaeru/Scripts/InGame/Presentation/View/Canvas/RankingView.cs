using System.Collections.Generic;
using Tsutaeru.Base.Presentation.View;
using UnityEngine;

namespace Tsutaeru.InGame.Presentation.View
{
    public sealed class RankingView : BaseCanvasGroupView
    {
        [SerializeField] private RectTransform viewport = default;
        [SerializeField] private RankingRecordView recordView = default;

        public void SetUp(List<Common.Data.Entity.TimeAttackRecordEntity> recordEntities)
        {
            foreach (var entity in recordEntities)
            {
                var record = Instantiate(recordView, viewport);
                record.SetData(entity);
            }
        }
    }
}