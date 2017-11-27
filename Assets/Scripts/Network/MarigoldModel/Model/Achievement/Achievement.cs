using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    [Table("Achievement")]
    public partial class Achievement : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        // 완료시 보상
        public AssetType AssetType { get; set; }
        public long AssetId { get; set; }
        public int AssetCount { get; set; }

        // 해당 조건을 카운트하는 History 참조
        public long HistoryId { get; set; }

        public long RequireCount { get; set; } // 업적을 달성하기 위해 필요한 수치

        public long DisplayOffset { get; set; } // 업적을 보여줄때 수치를 빼서 보여줄지 설정
    }
}
